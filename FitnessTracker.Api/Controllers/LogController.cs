
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

using FitnessTracker.Services;
using AutoMapper;
using FitnessTracker.DTO;
using FitnessTracker.Core.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace FitnessTracker.Controllers
{

    [Route("api/[Controller]")]
    public class LogController : Controller
    {
        private readonly ILogService _logService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public LogController(ILogService logService, ILogger<Log> logger, IMapper mapper)
        {
            _logService = logService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LogDTO>> Get()
        {
            try
            {
                var results = _logService.GetAllLogs();
                return Ok(_mapper.Map<IEnumerable<Log>, IEnumerable<LogDTO>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get logs: {ex}");
                return BadRequest("Failed to get logs");
            }

        }

        [HttpGet("{id}/{date}")]
        public ActionResult<IEnumerable<LogDTO>> Get(string id, DateTime date)
        {
            try
            {
                var results = _logService.GetLogsBySet(id, date);
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

                var result = _logService.GetLogById(id);
                return Ok(_mapper.Map<Log, LogDTO>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get logs: {ex}");
                return BadRequest("Failed to get logs");
            }
        }

        [HttpGet("{username}")]
        public ActionResult<LogDTO> Get(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username)) return NotFound();

                var results = _logService.GetLogsByUserName(username);
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
        public ActionResult Post([FromBody]SaveLogDTO log)
        {
            try
            {
                if(log == null) return NotFound();

                var newLog = _mapper.Map<SaveLogDTO, Log>(log);

                var logCreated = _logService.CreateLog(newLog);
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
        public ActionResult<LogDTO> Put(int id, [FromBody] SaveLogDTO log)
        {
            try
            {
                if (id < 1 || id != log.LogId)
                {
                    return BadRequest("Unable to update Log");
                }

                var tmpLog = _mapper.Map<SaveLogDTO, Log>(log, new Log());
                var logUpdated = _logService.UpdateLog(tmpLog);
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
                var log = _logService.DeleteLog(id);
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
