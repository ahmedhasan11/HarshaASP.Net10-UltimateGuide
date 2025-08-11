using System.ComponentModel.DataAnnotations;

namespace test_revision.Custom_Validators
{
	public class customAttribute:ValidationAttribute
	{
		//custom validations code
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			//write validations	
			if (value!=null)
			{
				return ValidationResult.Success;
			}
			else
			{
				return new ValidationResult("write error message here");
			}
						
		}
	}
}
