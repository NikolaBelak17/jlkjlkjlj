using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainerService.API.Entities;
using TrainerService.API.GrpcServices;
using TrainerService.API.Repositories;

namespace TrainerService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerRepository _repository;
        private readonly ReviewGrpcService _reviewGrpcService;
        private readonly IMapper _mapper;

        public TrainerController(ITrainerRepository repository, ReviewGrpcService reviewGrpcService, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _reviewGrpcService = reviewGrpcService ?? throw new ArgumentNullException(nameof(reviewGrpcService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Trainer>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetTrainers()
        {
            var trainers = await _repository.GetTrainers();

            foreach (var trainer in trainers)
            {
                var reviews = await _reviewGrpcService.GetReviews(trainer.FullName);
                trainer.Reviews = _mapper.Map<List<ReviewType>>(reviews.Reviews);
            }
            return Ok(trainers);
        }

        [HttpGet("{id}", Name = "GetTrainer")]
        [ProducesResponseType(typeof(Trainer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Trainer), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Trainer>> GetTrainerById(string id)
        {
            var trainer = await _repository.GetTrainer(id);
            if (trainer == null)
            {
                return NotFound(null);
            }
            else
            {
                var reviews = await _reviewGrpcService.GetReviews(trainer.FullName);
                trainer.Reviews = _mapper.Map<List<ReviewType>>(reviews.Reviews);
                return Ok(trainer);
            }
        }

        /*[Route("[action]/{minRating}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Trainer>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetTrainersByRating(int minRating)
        {
            var trainers = await _repository.GetTrainersByRating(minRating);
            return Ok(trainers);
        }*/

        [Route("[action]/{trainingType}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Trainer>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetTrainersByTrainingType(string trainingType)
        {
            var trainers = await _repository.GetTrainersByTrainingType(trainingType);
            foreach (var trainer in trainers)
            {
                var reviews = await _reviewGrpcService.GetReviews(trainer.FullName);
                trainer.Reviews = _mapper.Map<List<ReviewType>>(reviews.Reviews);

            }
            return Ok(trainers);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Trainer), StatusCodes.Status201Created)]
        public async Task<ActionResult<Trainer>> CreateTrainer(Trainer trainer)
        {
            await _repository.CreateTrainer(trainer);

            return CreatedAtRoute("GetTrainer", new { id = trainer.Id }, trainer);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Trainer), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTrainer([FromBody] Trainer trainer)
        {
            return Ok(await _repository.UpdateTrainer(trainer));
        }

        [HttpDelete("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Trainer), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteTrainer(string id)
        {
            return Ok(await _repository.DeleteTrainer(id));
        }
    }
}
