using Microsoft.AspNetCore.Identity;

namespace CitiesManager.Web.Identity
{
	public class ApplicationUser:IdentityUser<Guid>
	{
		//(ID,Email,UserName,PhoneNumber,PasswordHash)
	
		public string? PersonName { get; set; }
		public string? RefreshToken { get; set; }

		public DateTime RefreshTokenExpirationDateTime { get; set; }
	}
}
