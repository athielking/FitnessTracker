using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

using FitnessTracker.Api.Services;
using FitnessTracker.Core.Entities;
using FitnessTracker.DTO;
using FitnessTracker.Data.Managers;
using System.Security.Authentication;
using System.IO;
using Microsoft.Extensions.Configuration;
using FitnessTracker.Api.DTO;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace FitnessTracker.Api.Controllers
{
    [Route("api/[Controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        
        private readonly IConfiguration _config;        
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManger;

        public AuthController(
            ILogger<AuthController> logger,IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManger)
        {
            _config = config;
            _logger = logger;
            _userManager = userManager;
            _signInManger = signInManger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginDTO model)
        {
            try
            {            
                if(!ModelState.IsValid) return BadRequest(ModelState);

                var result = await _signInManger.PasswordSignInAsync(model.Username, model.Password, model.RemberMe, false);

                if (!result.Succeeded) return BadRequest("Invalid Login attempt");

                var tokenManager = new JwtTokenManager(
                    _config["JwtKey"],
                    double.Parse(_config["JwtExpireDays"]),
                    _config["JwtIssuer"]
                );

                var user = await _userManager.FindByNameAsync(model.Username);
                var token = tokenManager.GenerateJwtToken(user);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user: {ex}");
                return BadRequest("Failed to get user");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {              
                await _signInManger.SignOutAsync();
                return Ok();        
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to logout: {ex}");
                return BadRequest("Failed to logout");
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
       
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                    return BadRequest("Username is already being used");

                user = new User
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await  _userManager.CreateAsync(user, model.Password);
                
                if (!result.Succeeded)
                {
                    var err = result.Errors.ToList();
                    return BadRequest(new {errors=err});
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Register User: {ex}");
                return BadRequest(new { errorMessage = "Failed to Register User" });
            }
        }

    }
}
