using Microsoft.Extensions.Options;

namespace GamesAPI.Configurations
{
    public class JwtOptionsSetUp : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration config;

        public JwtOptionsSetUp(IConfiguration configuration)
        {
            config = configuration;
        }
        public void Configure(JwtOptions options)
        {
            config.GetSection("Jwt").Bind(options);
        }
    }
}
