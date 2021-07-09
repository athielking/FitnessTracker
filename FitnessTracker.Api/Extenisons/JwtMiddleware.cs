using AutoMapper.Configuration;
using FitnessTracker.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Api.Extenisons
{
    public class JwtMiddleware
    {
        //private readonly RequestDelegate _next;
        //private readonly string _jwtKey;

        //public JwtMiddleware(RequestDelegate next, JWTTokenValidators token)
        //{
        //    _next = next;
        //    _jwtKey = token.Key;
        //}

        //public async Task Invoke(HttpContext context, IConfiguration config, IUserService userService)
        //{
        //    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        //    if (token != null)
        //        attachUserToContext(context, userService, token, config.["jwtKey"]);

        //    await _next(context);
        //}

        //private void attachUserToContext(HttpContext context, IUserService userService, string token, string key)
        //{
        //    try
        //    {
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //        tokenHandler.ValidateToken(token, new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
        //            ClockSkew = TimeSpan.Zero
        //        }, out SecurityToken validatedToken);

        //        var jwtToken = (JwtSecurityToken)validatedToken;
        //        var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

        //        // attach user to context on successful jwt validation
        //        context.Items["User"] = userService.GetById(userId);
        //    }
        //    catch
        //    {
        //        // do nothing if jwt validation fails
        //        // user is not attached to context so request won't have access to secure routes
        //    }
        //}
    }
}
