using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class RegisterDTO
    {
		[Required(ErrorMessage ="Person Name can't be blank")]		
		public string PersonName { get; set; }
		[Required(ErrorMessage = "Email can't be blank")]
		[EmailAddress(ErrorMessage ="Email should be in proper email address Format") ]
		[Remote(action: "EmailValidation", controller:"Account",ErrorMessage ="Email Already Exists")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Phone Number can't be blank")]
		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }
		[Required(ErrorMessage = "Password can't be blank")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password Name can't be blank")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Confirm Password should match Password") ]
		public string ConfirmPassword { get; set; }

	}
}
