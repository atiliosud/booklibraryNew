using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookLibrary.Services
{
    public class JwtService
    {
        private readonly Configuration _configuration; // Instance of Configuration to access JwtPrivateKey and JwtExpires

        public JwtService(Configuration configuration)
        {
            _configuration = configuration;
        }

        public string Generate(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.JwtPrivateKey); // Access JwtPrivateKey through the instance of Configuration
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(_configuration.JwtExpires), // Access JwtExpires through the instance of Configuration
                Subject = GenerateClaims(user)
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            return ci;
        }
    }
}
