using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace test_revision.Custom_Validators
{
	public class MultipleValidationAttribute:ValidationAttribute
	{
		string otherpropertyname;
		public MultipleValidationAttribute()
		{

		}
		public MultipleValidationAttribute(string Otherpropertyname)
		{
			 otherpropertyname = Otherpropertyname;
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			//write validations here
			DateTime from_date = (DateTime)value; //if you already putthe attribute on from_date property
												  //we already have the other property name
												  //we need to get the value through reflection
			PropertyInfo? otherproperty = validationContext.ObjectType.GetProperty(otherpropertyname);

			DateTime to_date = Convert.ToDateTime(otherproperty.GetValue(validationContext.ObjectInstance));

			//if ()
			//{

			//}
			return null;
			
		}
	}
}
