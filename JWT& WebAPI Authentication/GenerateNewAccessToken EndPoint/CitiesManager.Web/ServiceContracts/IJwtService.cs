using CitiesManager.Web.DTO;
using CitiesManager.Web.Identity;
using System.Security.Claims;

namespace CitiesManager.Web.ServiceContracts
{
	public interface IJwtService
	{
		AuthenticationResponse CreateJwtToken(ApplicationUser user);

		ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
	}
}
