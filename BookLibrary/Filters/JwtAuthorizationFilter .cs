using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookLibrary.Filters
{
    public class JwtAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly Configuration _configuration;

        public JwtAuthorizationFilter(Configuration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                // Retrieving the JWT token from the request headers
                var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (token == null)
                {
                    // If token is not provided, return unauthorized response
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // Initializing JWT token handler and retrieving the JWT key from Configuration
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.JwtPrivateKey); // Accessing JWT key using Configuration

                // Validating the token
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out var validatedToken);

                // Extracting user ID from JWT claims
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "nameid").Value; // Change ClaimTypes.NameIdentifier to "nameid"

                // Here you can perform additional authorization logic based on the user's ID or any other claim

                // For simplicity, we'll just add the user's ID to the HttpContext so it's accessible in the controllers
                context.HttpContext.Items["UserId"] = userId;
            }
            catch (SecurityTokenException)
            {
                // If token validation fails, return unauthorized response
                context.Result = new UnauthorizedResult();
            }
            catch (Exception ex)
            {
                // If any other exception occurs, return internal server error response
                context.Result = new StatusCodeResult(500);
            }
        }
    }
}
