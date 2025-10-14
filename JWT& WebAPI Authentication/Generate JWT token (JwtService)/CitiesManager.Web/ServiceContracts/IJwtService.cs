using CitiesManager.Web.DTO;
using CitiesManager.Web.Identity;

namespace CitiesManager.Web.ServiceContracts
{
	public interface IJwtService
	{
		AuthenticationResponse CreateJwtToken(ApplicationUser user);
	}
}
