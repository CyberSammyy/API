using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.Services
{
    public class SessionService : ISessionService
    {
        private readonly AppSettings _appSettings;

        public SessionService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string CreateAuthToken(UserWithRoles user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var claims = user.Roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
