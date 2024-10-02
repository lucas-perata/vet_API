using System.Security.Claims;
using API.Data;
using API.Dtos;
using API.Dtos.Photo;
using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IPhotoService _photoService;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper,
            DataContext context,
            IConfiguration configuration,
            IPhotoService photoService
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
            _photoService = photoService;
        }

        [Authorize]
        [HttpGet("currentuser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Province = user.Province,
                Area = user.Area,
                Token = await _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null; // if email is not found, return false
        }

        [Authorize]
        [HttpGet("/deprecated")]
        public ActionResult<PetDto> GetUserPets()
        {
            var user = _userManager.FindUserByClaimsPrincipleWithPet(User).Result;
            var petsDto = user.Pets.Select(pet => _mapper.Map<Pet, PetDto>(pet)).ToList();
            return Ok(petsDto);
        }

        [HttpGet("user-type")]
        public async Task<IActionResult> GetUserType()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            //TODO: Add a repo to check type of user

            var isOwner = await _context.Owners.FindAsync(user.Id);
            if (isOwner != null)
                return Ok("owner");

            var isVet = await _context.Vets.FindAsync(user.Id);
            if (isVet != null)
                return Ok("vet");

            return NotFound();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                loginDto.Password,
                true
            );

            if (!result.Succeeded)
                return Unauthorized();

            return new UserDto
            {
                Email = user.Email,
                Area = user.Area,
                Province = user.Province,
                Token = await _tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
            };
        }

        [HttpPost("register-owner")]
        public async Task<ActionResult<UserDto>> Register([FromForm] RegisterDto registerDto)
        {
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Province = registerDto.Province,
                Area = registerDto.Area,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest("Problem registering user");

            var roleUser = await _userManager.AddToRoleAsync(user, "Owner");
            if (!roleUser.Succeeded)
                return BadRequest(result.Errors);

            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Area = user.Area,
                Province = user.Province,
                Token = await _tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
            };
        }

        [HttpPost("register-vet")]
        public async Task<ActionResult<UserDto>> RegisterVet([FromForm] RegisterDto registerDto)
        {
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Province = registerDto.Province,
                Area = registerDto.Area,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest("Problem registering user");

            var vet = new Vet { Id = user.Id, User = user };
            _context.Vets.Add(vet);

            var roleUser = await _userManager.AddToRoleAsync(user, "Vet");
            if (!roleUser.Succeeded)
                return BadRequest(result.Errors);

            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Area = user.Area,
                Province = user.Province,
                Token = await _tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
            };
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null)
                return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user.Photos.Count == 0)
                photo.IsMain = true;

            user.Photos.Add(photo);

            if (await _context.SaveChangesAsync() > 0)
                return _mapper.Map<PhotoDto>(photo);

            return BadRequest("Problem adding photo");
        }

        // [Authorize]
        // [HttpGet("all-vets")]
        // public async Task<ActionResult<List<VetDto>>> GetVets([FromQuery] UserParams userParams)
        // {
        //
        //     var email = User.FindFirstValue(ClaimTypes.Email);
        //     var user = await _userManager.FindByEmailAsync(email);
        //
        //     if (user is null)
        //         return Unauthorized();
        //
        //     // TODO: Interfaces + Repository
        //
        //     var nearVetsQuery = _context.Vets
        //  .Include(v => v.User)
        //  .Where(v => v.User.Area == user.Area)
        //  .ProjectTo<VetDto>(_mapper.ConfigurationProvider)
        //  .AsNoTracking();
        //
        //
        //
        //
        //     if (!await nearVetsQuery.AnyAsync())
        //     {
        //         var generalVetsQuery = _context.Vets
        //           .Include(v => v.User)
        //             .ProjectTo<VetDto>(_mapper.ConfigurationProvider)
        //             .AsNoTracking();
        //
        //         await PagedList<VetDto>.CreateAsync(generalVetsQuery, userParams.PageNumber, userParams.PageSize);
        //
        //     }
        //
        //     return await PagedList<VetDto>.CreateAsync(nearVetsQuery, userParams.PageNumber, userParams.PageSize);
        //
        // }
    }
}
