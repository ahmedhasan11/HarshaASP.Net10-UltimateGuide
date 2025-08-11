using e_commerce_Task_ModelBinding.CustomValidators;
namespace e_commerce_Task_ModelBinding.Models
{
	public class Order
	{
		public int? OrderID { get; set; }

		public DateTime OrderDate { get; set; }

		[ValidatorCustom]
		public double InvoicePrice { get; set; }
		public List<Product> Products { get; set; } = new List<Product>();
	}
}
