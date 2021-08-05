
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;

using AutoMapper;
using FitnessTracker.DTO;
using FitnessTracker.Core.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using FitnessTracker.Data.Repositories;
using FitnessTracker.Api.Extenisons;

namespace FitnessTracker.Controllers
{

    [Route("api/[Controller]")]
    public class LogController : Controller
    {
        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public LogController(ILogRepository logRepository, IUserRepository userRepository, ILogger<Log> logger, IMapper mapper)
        {
            _logRepository = logRepository;
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<LogDTO>> GetAll()
        {
            try
            {
                var userId = User.GetUserId();

                var results = _logRepository.GetAllLogs(userId);
                return Ok(_mapper.Map<IEnumerable<Log>, IEnumerable<LogDTO>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get logs: {ex}");
                return BadRequest("Failed to get logs");
            }

        }

        [HttpGet("{id:int}")]
        public ActionResult<LogDTO> Get(int id)
        {
            try
            {
                if (id == 0) return NotFound();

                var userId = User.GetUserId();
                var log = _logRepository.GetLogById(userId, id);

                return Ok(_mapper.Map<Log, LogDTO>(log));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get logs: {ex}");
                return BadRequest("Failed to get logs");
            }
        }

        [HttpGet("GetLogsBySetId/{setId}")]
        public ActionResult<IEnumerable<LogDTO>> GetLogsBySetId(string setId)
        {
            try
            {
                var userId = User.GetUserId();
                var logs = _logRepository.GetAllLogs(userId);

                if (!logs.Any()) return NotFound("Could not find logs");

                var results = logs.Where(c => c.SetId == setId).ToList();

                return Ok(_mapper.Map<IEnumerable<Log>, IEnumerable<LogDTO>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get logs: {ex}");
                return BadRequest("Failed to get logs");
            }

        }

        

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post([FromBody]SaveLogDTO logDto)
        {
            try
            {
                if(logDto == null) return NotFound();

                var userId = User.GetUserId();
                if (_userRepository.GetById(userId) == null && !userId.Equals(logDto.User.Id))
                {
                    return NotFound("Failed to create Log.  User not found");
                }

                var log = _mapper.Map<SaveLogDTO, Log>(logDto);

                var logCreated = _logRepository.CreateLog(log);
                var results = _mapper.Map<Log, LogDTO>(logCreated);

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create log: {ex}");
                return BadRequest("Failed to create log");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<LogDTO> Put(int id, [FromBody] SaveLogDTO logDto)
        {
            try
            {
                if (id < 1 || id != logDto.LogId)
                {
                    return BadRequest("Unable to update Log. Log id must have a value");
                }

                var userId = User.GetUserId();
                if (_userRepository.GetById(userId) == null && !userId.Equals(logDto.User.Id))
                {
                    return NotFound("Failed to update Log.  User not found");
                }

                var log = _mapper.Map(logDto, new Log());
                var logUpdated = _logRepository.Update(userId, log);
                var results = _mapper.Map<Log, LogDTO>(logUpdated);

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update log: {ex}");
                return BadRequest("Failed to update log");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();

                var log = _logRepository.Delete(userId, id);
                if (log == null)
                {
                    return NotFound("Cannot delete log. Cannot find Log");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete log: {ex}");
                return BadRequest("Failed to delete log");
            }
        }
    }
}
