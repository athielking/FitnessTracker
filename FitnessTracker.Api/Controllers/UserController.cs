using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AutoMapper;

using FitnessTracker.DTO;
using FitnessTracker.Api.Services;
using FitnessTracker.Api.Models;
using FitnessTracker.Api.DTO;
using Microsoft.Identity.Web.Resource;
using Microsoft.AspNetCore.Authorization;

namespace FitnessTracker.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<UserDTO>> Get()
        {
            try
            {
                var user = await _microsoftGraphService.GetLoggedInUser();
                return Ok(_mapper.Map<UserAD, UserDTO>(user));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user: {ex}");
                return BadRequest("Failed to get user");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidOutput))
                {
                    return NotFound($"Failed to get user: {nameof(id)} not found.");
                }

                var user = await _microsoftGraphService.GetUserById(id);
                return Ok(_mapper.Map<UserAD, UserDTO>(user));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user: {ex}");
                return BadRequest("Failed to get user");
            }
        }

        [HttpGet("Search/{key}")]
        public async Task<ActionResult<UserDTO>> Search(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    return NotFound($"Failed to get user: Search returned zero matches.");
                }

                var user = await _microsoftGraphService.Search(key);
                return Ok(_mapper.Map<UserAD, UserDTO>(user));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to find user: {ex}");
                return BadRequest("Failed to find user");
            }
        }

        [HttpPost]
        //[RequiredScope("write")]
        public async Task<ActionResult<UserDTO>> Post([FromBody] RegisterDTO user)
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
        public async Task<ActionResult<UserDTO>> Put(string id, [FromBody] UserDTO user)
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
