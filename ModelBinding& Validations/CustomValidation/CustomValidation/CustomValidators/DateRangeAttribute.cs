using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CustomValidation.CustomValidators
{

	#region

	//custom validation for multiple properties ,, we need to validate 2 or more properties together
	//for whatever comparing them or whatever you want 
	#endregion
	public class DateRangeAttribute:ValidationAttribute
	{
		public string OtherPropertyName { get; set; }
		public DateRangeAttribute(string otherpropertyname)
		{
			OtherPropertyName = otherpropertyname;
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
			if (value != null)
			{


				DateTime to_date = (DateTime)value;


				//using reflection to get the property
				PropertyInfo? otherPropertyName = validationContext.ObjectType.GetProperty(OtherPropertyName);
				DateTime from_date= Convert.ToDateTime(otherPropertyName.GetValue(validationContext.ObjectInstance)); //returns the value of the 
																													  //specific property based on the object instance

				if (from_date > to_date)
				{
					return new ValidationResult(ErrorMessage, new string[]
					{
						OtherPropertyName, validationContext.MemberName
					});
				}
				else {
					return ValidationResult.Success;
					}
			}
			return null;
		}

	}
}
