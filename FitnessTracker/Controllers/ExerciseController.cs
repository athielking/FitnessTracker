using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

using FitnessTracker.Data.Repositories;
using AutoMapper;
using FitnessTracker.DTO;
using FitnessTracker.Core.Entities;

namespace FitnessTracker.Controllers
{
    [Route("api/[Controller]")]
    public class ExerciseController : Controller
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ILogger<ExerciseController> _logger;
        private readonly IMapper _mapper;

        public ExerciseController(IExerciseRepository exerciseRepository, ILogger<ExerciseController> logger, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ExerciseDTO>> Get()
        {
            try
            {
                var results = _exerciseRepository.GetAllExercises();
                return Ok(_mapper.Map<IEnumerable<Exercise>, IEnumerable<ExerciseDTO>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get exercises: {ex}");
                return BadRequest("Failed to get exercises");
            }

        }

        [HttpGet("{name}")]
        public ActionResult<bool> Get(string name)
        {
            try
            {
                var results = _exerciseRepository.GetExerciseByName(name) != null;
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get exercise: {ex}");
                return BadRequest("Failed to get exercise");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<ExerciseDTO> Get(int id)
        {
            try
            {
                if (id == 0) return NotFound();

                var results = _exerciseRepository.GetExerciseById(id);
                return Ok(_mapper.Map<Exercise, ExerciseDTO>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get exercise: {ex}");
                return BadRequest("Failed to get exercise");
            }
        }

        [HttpPost]
        public ActionResult<ExerciseDTO> Post([FromBody]ExerciseDTO exercise)
        {
            try
            {
                var newexercise = _mapper.Map<ExerciseDTO, Exercise>(exercise);
                var results = _exerciseRepository.Create(newexercise);

                return Ok(_mapper.Map<Exercise, ExerciseDTO>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create exercise: {ex}");
                return BadRequest("Failed to create exercise");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ExerciseDTO> Put(int id, [FromBody] ExerciseDTO exercise)
        {
            try
            {
                if (id < 1 || id != exercise.ExerciseId)
                {
                    return BadRequest("Unable to update Exercise");
                }

                var newexercise = _mapper.Map<ExerciseDTO, Exercise>(exercise);
                var results = _exerciseRepository.Update(newexercise);

                return Ok(_mapper.Map<Exercise, ExerciseDTO>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update exercise: {ex}");
                return BadRequest("Failed to update exercise");
            }
                
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var customer = _exerciseRepository.Delete(id);
                if (customer == null)
                {
                    return NotFound("Cannot delete Exercise. Cannot find Exercise");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete exercise: {ex}");
                return BadRequest("Failed to delete exercise");
            }
        }
    }
}
