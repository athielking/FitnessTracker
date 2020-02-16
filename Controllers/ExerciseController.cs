using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

using FitnessTracker.Data.Entities;
using FitnessTracker.Data.Repositories;

namespace FitnessTracker.Controllers
{
    [Route("api/[Controller]")]
    public class ExerciseController : Controller
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ILogger<ExerciseController> _logger;

        public ExerciseController(IExerciseRepository exerciseRepository, ILogger<ExerciseController> logger)
        {
            _exerciseRepository = exerciseRepository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LogExercise>> Get()
        {
            try
            {
                return Ok(_exerciseRepository.GetAllExercises());
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
        public ActionResult<Log> Get(int id)
        {
            try
            {
                return Ok(_exerciseRepository.GetExerciseById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get exercise: {ex}");
                return BadRequest("Failed to get exercise");
            }
        }

        [HttpPost]
        public ActionResult<Log> Post([FromBody]Exercise exercise)
        {
            try
            {
                return Ok(_exerciseRepository.Create(exercise));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create exercise: {ex}");
                return BadRequest("Failed to create exercise");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Exercise> Put(int id, [FromBody] Exercise exercise)
        {
            if (id < 1 || id != exercise.ExerciseId)
            {
                return BadRequest("Unable to update Exercise");
            }

            return Ok(_exerciseRepository.Update(exercise));
        }

        [HttpDelete("{id}")]
        public ActionResult<Log> Delete(int id)
        {           
            var customer = _exerciseRepository.Delete(id);
            if (customer == null)
            {
                return StatusCode(404, "Cannot delete Exercise. Cannot find Exercise");
            }

            return NoContent();
        }
    }
}
