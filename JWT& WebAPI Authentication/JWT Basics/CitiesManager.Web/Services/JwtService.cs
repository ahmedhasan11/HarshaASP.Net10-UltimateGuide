using CitiesManager.Web.DTO;
using CitiesManager.Web.Identity;
using CitiesManager.Web.ServiceContracts;

namespace CitiesManager.Web.Services
{
	public class JwtService : IJwtService
	{
		private readonly IConfiguration _configuration;
		public JwtService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public AuthenticationResponse CreateJwtToken(ApplicationUser user)
		{
			DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble( _configuration["Jwt:EXPIRATION_MINUTES"]));

		}
	}
}
