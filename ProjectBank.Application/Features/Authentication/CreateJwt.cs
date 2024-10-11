using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Register_Login
{
    public class CreateJwt(IConfiguration configuration)
    {
        public string Handle(Account account)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:Secret"] ?? string.Empty);
            var identity = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Role, account.Role.ToString() ?? string.Empty),
                new Claim(ClaimTypes.Name, account.Name),
            ]);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
