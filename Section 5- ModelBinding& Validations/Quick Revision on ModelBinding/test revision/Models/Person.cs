using System.ComponentModel.DataAnnotations;

namespace test_revision.Models
{
	public class Person:IValidatableObject
	{
		//here we will use the IValidateableObject
		public int Id { get; set; }
		public string Name { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			//write your validations here
			if (Id == null && Name==null)
			{
				yield return new ValidationResult("error message here");
			}
			//you can write multiple conditions and return them using yield
		}
	}
}
