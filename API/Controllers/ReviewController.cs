using System.Security.Claims;
using API.Dtos.Review;
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
    public class ReviewController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ReviewRepository _reviewRepository;
        private readonly UserManager<AppUser> _userManager;

        public ReviewController(
            IMapper mapper,
            ReviewRepository reviewRepository,
            UserManager<AppUser> userManager
        )
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            var review = await _reviewRepository.GetReview(id);

            if (review is null)
                return NotFound();

            return _mapper.Map<ReviewDto>(review);
        }

        // TODO: Crear metodos separados para que los usuarios vean reseñas -> por un lado
        // vets y por el otro dueños
        [HttpGet("vet/{vetId}")]
        public async Task<ActionResult<List<ReviewDto>>> GetAllReviewsForVet(
            string vetId,
            [FromQuery] UserParams userParams
        )
        {
            var vet = await _userManager.FindByIdAsync(vetId);

            if (vet is null)
                return NotFound();

            var reviews = await _reviewRepository.GetReviewsForVet(vetId, userParams);

            if (reviews is null)
                return NotFound("Vet has no reviews");

            Response.AddPaginationHeader(
                new PaginationHeader(
                    reviews.CurrentPage,
                    reviews.PageSize,
                    reviews.TotalCount,
                    reviews.TotalPages
                )
            );

            return Ok(reviews);
        }

        // Solo los owners pueden dejar reseñas
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateReview(CreateReviewDto createReviewDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var review = new Review
            {
                Stars = createReviewDto.Stars,
                Body = createReviewDto.Body,
                OwnerId = user.Id,
                VetId = createReviewDto.VetId
            };

            _reviewRepository.AddReview(review);

            if (await _reviewRepository.Complete())
                return Ok(_mapper.Map<ReviewDto>(review));

            return BadRequest("Failed to create review");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReview(int id, UpdateReviewDto updateReviewDto)
        {
            var review = await _reviewRepository.GetReview(id);

            if (review is null)
                return NotFound();

            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (review.OwnerId != user.Id)
                return BadRequest("Not your review");

            _mapper.Map(updateReviewDto, review);

            _reviewRepository.UpdateReview(review);

            if (await _reviewRepository.Complete())
                return Ok();

            return BadRequest("Failed to update review");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            var review = await _reviewRepository.GetReview(id);

            if (review is null)
                return NotFound();

            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (review.OwnerId != user.Id)
                return BadRequest("Not your review");

            var result = _reviewRepository.DeleteReview(review);

            if (!result)
                return BadRequest("There was a problem deleting the review");

            return NoContent();
        }
    }
}
