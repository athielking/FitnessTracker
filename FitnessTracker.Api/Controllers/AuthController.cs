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
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

using Microsoft.Extensions.Options;
using FitnessTracker.Api.Configuration;

namespace FitnessTracker.Api.Controllers
{
    [Route("api/[Controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        
        private readonly JwtSetting _jwtSettings;        
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManger;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AuthController(
            ILogger<AuthController> logger, IOptions<JwtSetting> jwtSettings, UserManager<User> userManager, SignInManager<User> signInManger, IMapper mapper, IEmailService emailService)
        {
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
            _mapper = mapper;
            _signInManger = signInManger;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            try
            {            
                if(!ModelState.IsValid) return BadRequest(ModelState);

                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null) return BadRequest();

                var result = await _signInManger.PasswordSignInAsync(user, model.Password, model.ReMemberMe, false);

                if (!result.Succeeded) return BadRequest("Invalid Login attempt");

                var tokenManager = new JwtTokenManager(
                    _jwtSettings.JwtKey,
                    _jwtSettings.JwtExpireDays,
                    _jwtSettings.JwtIssuer
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

        [HttpPost("Logout")]
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
                if (!ModelState.IsValid || model == null) return BadRequest(ModelState);
       
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                    return BadRequest("Username is already being used");

                user = new User()
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);            
                if (!result.Succeeded)
                {
                    var err = result.Errors.ToList();
                    return BadRequest(new {errors=err});
                }
                //await _userManager.AddToRoleAsync(user, "user");
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Register User: {ex}");
                return BadRequest(ex.StackTrace);
            }
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await _userManager.FindByNameAsync(resetPasswordDto.Username);
            if (user == null)
                return BadRequest("Invalid Request");
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

            if (user == null)
                return BadRequest("Invalid Request");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var param = new Dictionary<string, string>
            {
                {"token", token },
                {"email", forgotPasswordDto.Email }
            };

            var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientURI, param);
            _emailService.SendEmail(user.Email, "Reset password token", callback);

            return Ok();
        }

    }
}
