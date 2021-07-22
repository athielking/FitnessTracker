using AutoMapper;
using FitnessTracker.Api.DTO;
using FitnessTracker.Core.Entities;
using FitnessTracker.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Api.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly ILogger _logger;
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IMapper _mapper;

        public WorkoutController(ILogger<WorkoutController> logger, IWorkoutRepository workoutRepository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _workoutRepository = workoutRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Workout>> Get()
        {
            try
            {
                var results = _workoutRepository.GetAllWorkouts();
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get workouts: {ex}");
                return BadRequest("Failed to get workouts");
            }

        }

        [HttpGet("{id}")]
        public ActionResult<Workout> Get(string id)
        {
            try
            {
                var results = _workoutRepository.GetWorkoutById(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get workout: {ex}");
                return BadRequest("Failed to get workout");
            }

        }

        [HttpPost]
        public ActionResult<WorkoutDTO> Post([FromBody] WorkoutDTO workout)
        {
            try
            {
                if (workout == null)
                {
                    return NotFound("User could not saved");
                }

                var newworkout = _mapper.Map<WorkoutDTO, Workout>(workout);
                var results = _workoutRepository.Create(newworkout);

                return Ok(_mapper.Map<Workout, WorkoutDTO>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create User: {ex}");
                return BadRequest("Failed to create User");
            }
        }
    }
}
