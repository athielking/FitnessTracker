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
using FitnessTracker.Api.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Claims;

namespace FitnessTracker.Api.Controllers
{
    [Route("api/[Controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        
        private readonly IConfiguration _config;        
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManger;
        private readonly IMapper _mapper;

        public AuthController(
            ILogger<AuthController> logger,IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManger, IMapper mapper)
        {
            _config = config;
            _logger = logger;
            _userManager = userManager;
            _signInManger = signInManger;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            try
            {            
                if(!ModelState.IsValid) return BadRequest(ModelState);

                var tmpuser = new User()
                {
                    UserName = model.UserName
                };

                var user = await _userManager.FindByNameAsync(model.UserName);

                var result = await _signInManger.PasswordSignInAsync(user, model.Password, model.ReMemberMe, false);


                if (!result.Succeeded) return BadRequest("Invalid Login attempt");

                var tokenManager = new JwtTokenManager(
                    _config["JwtKey"],
                    double.Parse(_config["JwtExpireDays"]),
                    _config["JwtIssuer"]
                );

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
                _logger.LogError($"Failed to logout user: {ex}");
                return BadRequest("Failed to logout user");
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

                user = new User()
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                //user = _mapper.Map<User>(model);
                ///user.PasswordHash = string.Empty;
                var result = await _userManager.CreateAsync(user, model.Password);            
                if (!result.Succeeded)
                {
                    var err = result.Errors.ToList();
                    return BadRequest(new {errors=err});
                }
                //await _userManager.AddToRoleAsync(user, "user");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Register User: {ex}");
                return BadRequest(new { errorMessage = "Failed to Register User" });
            }
        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordDTO model)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Sign in User: {ex}");
                return BadRequest(new { errorMessage = "Failed to Sign in User" });
            }
        }

    }
}
