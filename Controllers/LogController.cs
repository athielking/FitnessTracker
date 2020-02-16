using FitnessTracker.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

using FitnessTracker.Services;

namespace FitnessTracker.Controllers
{
    [Route("api/[Controller]")]
    public class LogController : Controller
    {
        private readonly ILogService _logService;
        private readonly ILogger _logger;
        
        public LogController(ILogService logService, ILogger<Log> logger)
        {
            _logService = logService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Log>> Get()
        {
            try
            {
                var results = _logService.GetAllLogs();
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get logs: {ex}");
                return BadRequest("Failed to get logs");
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id == 0) return NotFound();

                var results = _logService.GetLogsByUserId(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get logs: {ex}");
                return BadRequest("Failed to get logs");
            }
        }

        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username)) return NotFound();
                
                var results = _logService.GetLogsByUserName(username);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get logs: {ex}");
                return BadRequest("Failed to get logs");
            }
        }

        [HttpPost]
        public ActionResult<Log> Post([FromBody]Log log)
        {
            try
            { 
                return Ok(_logService.CreateLog(log));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create log: {ex}");
                return BadRequest("Failed to create log");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Log> Put(int id, [FromBody] Log log)
        {
            try
            {
                if (id < 1 || id != log.LogId)
                {
                    return BadRequest("Unable to update Log");
                }

                return Ok(_logService.UpdateLog(log));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update log: {ex}");
                return BadRequest("Failed to update log");
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<Log> Delete(int id)
        {
            var customer = _logService.DeleteLog(id);
            if (customer == null)
            {
                return StatusCode(404, "Cannot delete Log. Cannot find Log");
            }

            return NoContent();
        }

    }
}
