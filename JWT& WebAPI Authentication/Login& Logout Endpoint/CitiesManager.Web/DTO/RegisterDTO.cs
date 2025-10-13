using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CitiesManager.Web.DTO
{
	public class RegisterDTO
	{
		[Required(ErrorMessage ="PersonName can't be empty")]
		[MinLength(3)]
		public string PersonName { get; set; }


		[Required(ErrorMessage = "Email can't be empty")]
		[EmailAddress(ErrorMessage = "Email should be on Email Format")]
		[Remote(action: "IsEmailAlreadyRegistered", controller:"Account", ErrorMessage ="Email is Already Registered ")]
		public string Email { get; set; }


		[Required(ErrorMessage = "Phone Number can't be empty")]
		[RegularExpression("^[0-9]*$", ErrorMessage = "Phone Number should contain digits only")]
		public string PhoneNumber { get; set; }


		[Required(ErrorMessage = "Password can't be empty")]
		public string Password { get; set; }


		[Required(ErrorMessage = "Confirm Password can't be empty")]
		[Compare("Password", ErrorMessage ="Password and Compare Password should be the same")]
		public string ConfirmPassword { get; set; }

	
	}
}
