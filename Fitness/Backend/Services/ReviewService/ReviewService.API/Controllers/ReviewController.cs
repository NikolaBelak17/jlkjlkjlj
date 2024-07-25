using Microsoft.AspNetCore.Mvc;
using ReviewService.Common.DTOs;
using ReviewService.Common.Repositories;

namespace ReviewService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _repository;

        public ReviewController(IReviewRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        [HttpGet("trainerName", Name ="GetReviews")]
        [ProducesResponseType(typeof(ReviewDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews(string trainerName)
        {
            var reviews = await _repository.GetReviews(trainerName);
            if (reviews == null || !reviews.Any())
            {
                return NotFound();
            }
            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReviewDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult<ReviewDTO>> CreateReview([FromBody] CreateReviewDTO reviewDTO)
        {
            await _repository.CreateReview(reviewDTO);
            var reviews = await _repository.GetReviews(reviewDTO.TrainerName);
            var newReview = reviews.FirstOrDefault(r => r.Comment == reviewDTO.Comment);
            return CreatedAtRoute("GetReviews", new { trainerName = reviewDTO.TrainerName }, newReview);
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> UpdateReview([FromBody] UpdateReviewDTO review)
        {
            return Ok(await _repository.UpdateReview(review));
        }

        [HttpDelete(Name = "DeleteReview")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteReview(string reviewId)
        {
            return Ok(await _repository.DeleteReview(reviewId));
        }
    }
}
