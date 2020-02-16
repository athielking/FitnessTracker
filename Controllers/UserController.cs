using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

using FitnessTracker.Data.Entities;
using FitnessTracker.Data.Repositories;

namespace FitnessTracker.Controllers
{
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            try
            {
                return Ok(_userRepository.GetAllUsers());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get users: {ex}");
                return BadRequest("Failed to get users");
            }

        }

        [HttpGet("{name}")]
        public ActionResult<User> Get(string name)
        {
            try
            {
                return Ok(_userRepository.GetByUsernameWithAllLogs(name));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user: {ex}");
                return BadRequest("Failed to get user");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<User> Get(int id)
        {
            try
            {
                return Ok(_userRepository.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user: {ex}");
                return BadRequest("Failed to get user");
            }
        }

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    throw new InvalidDataException("User could not saved");
                }

                var results = _userRepository.Create(user);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create User: {ex}");
                return BadRequest("Failed to create User");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {          
            try
            {
                if (id < 1 || id != user.Id)
                {
                    return BadRequest("Parameter Id and customer ID must be the same");
                }

                return Ok(_userRepository.Update(user));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update user: {ex}");
                return BadRequest("Failed to update user");
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            var customer = _userRepository.Delete(id);
            if (customer == null)
            {
                return StatusCode(404, "Cannot delete user. User with ID " + id + " not found");
            }

            return NoContent();
        }
    }
}
