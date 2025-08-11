using System.ComponentModel.DataAnnotations;

namespace CustomValidation.CustomValidators
{
	public class DateRangeAttribute:ValidationAttribute
	{
		public string OtherPropertyName { get; set; }
		public DateRangeAttribute(string otherpropertyname)
		{
			OtherPropertyName=otherpropertyname
		}
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			//we talked about the value parameter before
			//but we didnt talk or use the validationContext parameter

			//validationContext parameter have informartion about the model properties 
			//and model calss and model object

			//you have property in the validationContext parameter called ObjectInstance
			//objectinstance is like an object of the preson class that modelbinding did
			//datatype of it is object so you cant get actual properties values directly
			//validationContext.ObjectInstance;
			//so now we need a solution that let us read the property actual value
			//we can read actual values through concept called reflection --.contains metadata of the objects
			PropertyInfo? OtherPropertyName = validationContext.ObjectType.GetProperty(OtherPropertyName);

			return base.IsValid(value, validationContext);

			//stop 10:35
		}

	}
}
