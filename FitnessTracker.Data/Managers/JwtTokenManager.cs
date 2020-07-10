using FitnessTracker.Api.Models;
using FitnessTracker.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FitnessTracker.Data.Managers
{
    public class JwtTokenManager
    {
        private readonly string _jwtKey;
        private readonly double _jwtExpireDays;
        private readonly string _jwtIssuer;

        public JwtTokenManager(string jwtKey, double jwtExpireDays, string jwtIssuer)
        {
            _jwtKey = jwtKey;
            _jwtExpireDays = jwtExpireDays;
            _jwtIssuer = jwtIssuer;
        }

        public JWTToken GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("username", user.UserName),
                new Claim("id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_jwtExpireDays);

            var token = new JwtSecurityToken(
                _jwtIssuer,
                _jwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JWTToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = user.UserName,
                Expiration = token.ValidTo
            };
        }
    }
}
