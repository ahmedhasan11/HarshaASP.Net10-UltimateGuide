using System.ComponentModel.DataAnnotations;
using CustomValidation.CustomValidators;

namespace CustomValidation.Models
{
	#region IValidateableObject
	//here we need to make a validation code for a speecific model (not reusable)
	//so you will write this custom validation in the same model class instead of outer class
	#endregion
	public class Person:IValidatableObject
	{
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{//write validationcode here
		 //for example we want the user to either send age or datebirth (can't send both)
			if (Age==null && DateBirth==null)
			{
				//keyword yield let you return multiple things ,,making them IEnumerable
				//we should always use it in that sequence
				yield return new ValidationResult("");
			}

		}
		public int? Age { get; set; }
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

		public override string ToString()
		{
			return $"Person object - Person name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
		}


	}
}
