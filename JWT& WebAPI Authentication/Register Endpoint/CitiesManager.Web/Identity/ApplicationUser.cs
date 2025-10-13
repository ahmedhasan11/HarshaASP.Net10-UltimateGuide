using Microsoft.AspNetCore.Identity;

namespace CitiesManager.Web.Identity
{
	public class ApplicationUser:IdentityUser<Guid>
	{
		//(ID,Email,UserName,PhoneNumber,PasswordHash)
	
		public string? PersonName { get; set; }
	}
}
