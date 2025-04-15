using System.ComponentModel.DataAnnotations;
using CustomValidation.CustomValidators;

namespace CustomValidation.Models
{
	public class Person
	{
		[Required(ErrorMessage = "custom msg")]
		[RegularExpression("^[A-Za-z .]$")] //specify some values that only accepted
		public string? PersonName { get; set; }

		[EmailAddress]
		public string? Email { get; set; }
		[Phone]
		public string? Phone { get; set; }
		[Required]
		public string? Password { get; set; }

		[Required]
		[Compare("Password")] //password and confirm password should be the same
		public string? ConfirmPassword { get; set; }
		public double? Price { get; set; }

		[CustomValidate]
		public DateTime DateBirth { get; set; }
		public DateTime from_date { get; set; }

		[DateRange("from_date")]
		public DateTime to_date { get; set; }


		public override string ToString()
		{
			return $"Person object - Person name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
		}
	}
}
