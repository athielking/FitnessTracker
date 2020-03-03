using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

using FitnessTracker.Data.Repositories;
using AutoMapper;
using FitnessTracker.DTO;
using FitnessTracker.Core.Entities;

namespace FitnessTracker.Controllers
{
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> Get()
        {
            try
            {
                var results = _userRepository.GetAllUsers();
                return Ok(_mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get users: {ex}");
                return BadRequest("Failed to get users");
            }

        }

        [HttpGet("{name}")]
        public ActionResult<UserDTO> Get(string name)
        {
            try
            {
                var results = _userRepository.GetByUsername(name);
                return Ok(_mapper.Map<User, UserDTO>(results));
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
                var results = _userRepository.GetById(id);
                return Ok(_mapper.Map<User, UserDTO>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user: {ex}");
                return BadRequest("Failed to get user");
            }
        }

        [HttpPost]
        public ActionResult<UserDTO> Post([FromBody] UserDTO user)
        {
            try
            {
                if (user == null)
                {
                    throw new InvalidDataException("User could not saved");
                }

                var newuser = _mapper.Map<UserDTO, User>(user);
                var results = _userRepository.Create(newuser);

                return Ok(_mapper.Map<User, UserDTO>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create User: {ex}");
                return BadRequest("Failed to create User");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<UserDTO> Put(int id, [FromBody] UserDTO user)
        {          
            try
            {
                if (id < 1 || id != user.Id)
                {
                    return BadRequest("Parameter Id and customer ID must be the same");
                }
                var tmp = _mapper.Map<UserDTO, User>(user);
                var results = _userRepository.Update(tmp);

                return Ok(_mapper.Map<User, UserDTO>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update user: {ex}");
                return BadRequest("Failed to update user");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _userRepository.Delete(id);
                if (user == null)
                {
                    return NotFound("Cannot delete User. Cannot find User");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete user: {ex}");
                return BadRequest("Failed to delete user");
            }
        }

    }
}
