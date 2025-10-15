using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CitiesManager.Web.DTO
{
	public class LoginDTO
	{
		[Required(ErrorMessage = "Email can't be empty")]
		[EmailAddress(ErrorMessage = "Email should be on Email Format")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password can't be empty")]
		public string Password { get; set; }
	}
}
