using System.ComponentModel.DataAnnotations;

namespace CustomValidation.CustomValidators
{
	//inheritthe class from the ValidationAttributeClass 
	public class CustomValidateAttribute:ValidationAttribute
	{
		//in the ValidationAttribute class there is a method called IsValid
		//you have to override this IsValid method

		public int minidateyear { get; set; } = 2000; 
		public CustomValidateAttribute()
		{

		}
		public CustomValidateAttribute(int minimumYear)
		{
			minidateyear=minimumYear;
		}
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			//value parameter is the actual value that is entered in the request
			//now you need to return ValidationRequest.Success if condition is true
			//return new ValidationResult("") if condition not happened

			if (value != null)
			{
				DateTime date = (DateTime)value;
				if (minidateyear>= 2000)
				{
					return new ValidationResult("dob year should be lower than 2000 should be lower than 2000");
				}
				else
				{
					return ValidationResult.Success;
				}

			}
			return null;
		}
	}
}
