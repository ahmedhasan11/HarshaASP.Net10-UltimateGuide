using e_commerce_Task_ModelBinding.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace e_commerce_Task_ModelBinding.CustomValidators
{
	public class ValidatorCustom:ValidationAttribute
	{
		public ValidatorCustom()
		{

		}
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			double orderInvoice = (double)value;

			if (value!=null)
			{
				PropertyInfo? prop= validationContext.ObjectType.GetProperty(nameof(Order.Products));

				List<Product> productlist= (List<Product>)prop.GetValue(validationContext.ObjectInstance) ;

				double totalprice=0;

				foreach (Product product in productlist)
				{
					totalprice += product.Price * product.Quantity;
				}

				if (orderInvoice == totalprice)
				{
					return ValidationResult.Success;
				}
				else
				{
					return new ValidationResult("Invoice Price should be equal to the total cost of all products (i.e. {0}) in the order.");
				}





			}

			return base.IsValid(value, validationContext);
		}
	}
}
