using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AutoMapper;

using FitnessTracker.DTO;
using FitnessTracker.Api.Services;
using FitnessTracker.Api.Models;
using FitnessTracker.Api.DTO;

namespace FitnessTracker.Controllers
{
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IMicrosoftGraphService _microsoftGraphService;

        public UserController(ILogger<UserController> logger, IMapper mapper, IMicrosoftGraphService microsoftGraphService)
        {
            _logger = logger;
            _mapper = mapper;
            _microsoftGraphService = microsoftGraphService;
        }

        [HttpGet]
        public async Task<ActionResult<UserAD>> Get()
        {
            try
            {
                var user = await _microsoftGraphService.GetLoggedInUser();
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user: {ex}");
                return BadRequest("Failed to get user");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserAD>> Get(string id)
        {
            try
            {
                var user = await _microsoftGraphService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user: {ex}");
                return BadRequest("Failed to get user");
            }
        }

        [HttpGet("Search/{key}")]
        public async Task<ActionResult<UserAD>> Search(string key)
        {
            try
            {
                var results = await _microsoftGraphService.Search(key);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to find user: {ex}");
                return BadRequest("Failed to find user");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserAD>> Post([FromBody] RegisterDTO user)
        {
            try
            {
                if (user == null)
                {
                    return NotFound("User could not be created");
                }

                if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.ConfirmPassword) && !user.ConfirmPassword.Equals(user.Password))
                {
                    return BadRequest("Failed to create User");
                }

                var newuser = _mapper.Map<RegisterDTO, UserAD>(user);
                var results = await _microsoftGraphService.Create(newuser);

                return Ok(_mapper.Map<UserAD, UserDTO>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create User: {ex}");
                return BadRequest("Failed to create User");
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdatePassword([FromBody] string password) {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return NotFound("Failed to update password");
                }

                return await _microsoftGraphService.UpdatePassword(password);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update password: {ex}");
                return BadRequest("Failed to update password");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<UserAD>> Put(string id, [FromBody] UserDTO user)
        {
            try
            {
                if (user == null)
                {
                    return NotFound("User could not be updated");
                }

                var newuser = _mapper.Map<UserDTO, UserAD>(user);
                var results = await _microsoftGraphService.Update(newuser);

                return Ok(_mapper.Map<UserAD, UserDTO>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create User: {ex}");
                return BadRequest("Failed to create User");
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var results = await _microsoftGraphService.Delete(id);
                if (!results)
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
