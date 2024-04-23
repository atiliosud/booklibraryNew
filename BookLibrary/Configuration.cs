using Microsoft.Extensions.Configuration;

namespace BookLibrary
{
    public class Configuration
    {
        private readonly IConfiguration _configuration;

        public Configuration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string JwtPrivateKey => _configuration["Jwt:Secret"];

        // JwtExpires in hours
        public int JwtExpires => _configuration.GetValue<int>("Jwt:Expires");
    }
}
