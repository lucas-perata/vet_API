using System.Security.Claims;
using API.Dtos.Spending;
using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using API.Helpers;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SpendingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SpendingRepository _spendingRepository;
        private readonly PetRepository _petRepository;
        private readonly UserManager<AppUser> _userManager;

        public SpendingController(
            PetRepository petRepository,
            IMapper mapper,
            SpendingRepository spendingRepository,
            UserManager<AppUser> userManager
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _spendingRepository = spendingRepository;
            _petRepository = petRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpendingDto>> GetSpending(int id)
        {
            var spending = await _spendingRepository.GetSpending(id);

            if (spending is null)
                return NotFound();

            return Ok(_mapper.Map<SpendingDto>(spending));
        }

        [HttpGet("all-spendings")]
        public async Task<ActionResult<PagedList<SpendingDto>>> GetAllSpendingsForUser(
            [FromQuery] UserParams userParams
        )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var spendings = await _spendingRepository.GetSpendingsForOwner(user.Id, userParams);

            Response.AddPaginationHeader(
                new PaginationHeader(
                    spendings.CurrentPage,
                    spendings.PageSize,
                    spendings.TotalCount,
                    spendings.TotalPages
                )
            );

            return Ok(spendings);
        }

        [HttpGet("sum-spendings")]
        public async Task<ActionResult<PagedList<SpendingDto>>> GetAllSumSpendings()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var spendingSum = await _spendingRepository.GetTotalSpendingForOwner(user.Id);

            return Ok(spendingSum);
        }

        [HttpGet("sum-spendings-year")]
        public async Task<ActionResult<decimal>> GetAllSpendingsSumYear()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var spendingSum = await _spendingRepository.GetTotalSpendingForOwnerCurrentYear(
                user.Id
            );

            return Ok(spendingSum);
        }

        [HttpGet("sum-previous-months")]
        public async Task<ActionResult<List<MonthlySpendingSummaryDto>>> GetPreviousMonthsSum()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var spendingSum = await _spendingRepository.GetSumSixMonthts(user.Id);

            return Ok(spendingSum);
        }

        [HttpGet("monthly")]
        public async Task<ActionResult<PagedList<SpendingDto>>> GetAllSpendingsMonthly(
            [FromQuery] UserParams userParams
        )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var spendings = await _spendingRepository.GetSpendingsForOwnerMonthly(
                user.Id,
                userParams
            );

            if (spendings is null)
                return NotFound();

            Response.AddPaginationHeader(
                new PaginationHeader(
                    spendings.CurrentPage,
                    spendings.PageSize,
                    spendings.TotalCount,
                    spendings.TotalPages
                )
            );

            return Ok(spendings);
        }

        [HttpGet("monthly-sum")]
        public async Task<ActionResult<MonthlySpendingSumDto>> GetSumSpendingsMonthly(
            [FromQuery] int month,
            [FromQuery] int year
        )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var currentMonth = await _spendingRepository.GetTotalSpendingForOwnerMonthly(
                user.Id,
                month,
                year
            );
            var lastY = year;
            var lastMonth = await _spendingRepository.GetTotalSpendingForOwnerMonthly(
                user.Id,
                month == 1 ? 12 : month - 1,
                month == 1 ? lastY - 1 : year
            );

            var sum = new MonthlySpendingSumDto
            {
                CurrentMonth = currentMonth,
                LastMonth = lastMonth,
            };

            return Ok(sum);
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<PagedList<SpendingDto>>> GetSpendingsForCategory(
            [FromQuery] UserParams userParams,
            SpendingCategory category
        )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var spendings = await _spendingRepository.GetSpendingsForCategory(
                user.Id,
                category,
                userParams
            );

            if (spendings is null)
                return NotFound();

            Response.AddPaginationHeader(
                new PaginationHeader(
                    spendings.CurrentPage,
                    spendings.PageSize,
                    spendings.TotalCount,
                    spendings.TotalPages
                )
            );

            return Ok(spendings);
        }

        [HttpGet("category/sum")]
        public async Task<ActionResult<List<CategorySumDto>>> GetSumForCategory()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var sum = await _spendingRepository.GetCategorySpendingForCurrentMonth(user.Id);

            if (sum is null)
                return NotFound();

            return Ok(sum);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<PagedList<SpendingDto>>> GetSpendingsForRange(
            DateTime startDate,
            DateTime endDate,
            UserParams userParams
        )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var spendings = await _spendingRepository.GetSpendingsForDateRange(
                user.Id,
                startDate,
                endDate,
                userParams
            );

            if (spendings is null)
                return NotFound();

            Response.AddPaginationHeader(
                new PaginationHeader(
                    spendings.CurrentPage,
                    spendings.PageSize,
                    spendings.TotalCount,
                    spendings.TotalPages
                )
            );

            return Ok(spendings);
        }

        [HttpGet("pet-expenses/{petId}")]
        public async Task<ActionResult<PagedList<SpendingDto>>> GetPetExpenses(
            int petId,
            [FromQuery] UserParams userParams
        )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var spendings = await _spendingRepository.GetSpendingsForPet(
                petId,
                user.Id,
                userParams
            );

            if (spendings is null)
                return NotFound();

            Response.AddPaginationHeader(
                new PaginationHeader(
                    spendings.CurrentPage,
                    spendings.PageSize,
                    spendings.TotalCount,
                    spendings.TotalPages
                )
            );

            return Ok(spendings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpending(CreateSpendingDto createSpendingDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var spending = new Spending
            {
                OwnerId = user.Id,
                Description = createSpendingDto.Description,
                PetId = createSpendingDto.PetId,
                Amount = createSpendingDto.Amount,
                Date = createSpendingDto.Date,
                Category = createSpendingDto.Category,
            };

            if (createSpendingDto.PetId > 0)
            {
                var pet = await _petRepository.GetPet(createSpendingDto.PetId);
                if (pet is null)
                    return NotFound("Pet not found");
            }

            _spendingRepository.AddSpending(spending);

            if (await _spendingRepository.Complete())
                return Ok(_mapper.Map<SpendingDto>(spending));

            return BadRequest("Failed to create expense");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSpending(int id, UpdateSpendingDto updateSpendingDto)
        {
            var spending = await _spendingRepository.GetSpending(id);

            if (spending is null)
                return NotFound();

            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (spending.OwnerId != user.Id)
                return Unauthorized();

            _mapper.Map(updateSpendingDto, spending);

            _spendingRepository.UpdateSpending(spending);

            if (await _spendingRepository.Complete())
                return Ok();

            return BadRequest("Failed to update");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSpending(int id)
        {
            var spending = await _spendingRepository.GetSpending(id);

            if (spending is null)
                return NotFound();

            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (spending.OwnerId != user.Id)
                return Unauthorized();

            var result = _spendingRepository.DeleteSpending(spending);

            if (!result)
                return BadRequest("There was a problem deleting the pet");

            return NoContent();
        }
    }
}
