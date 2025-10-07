using System.ComponentModel.DataAnnotations;

namespace API_Task.Models
{
	public class Order
	{
		[Key]
		public Guid OrderID { get; set; }
		// OrderNumber should be auto-generated using a sequential number
		//The year should be automatically generated as current year.
		public string OrderNumber { get; set; }
		[Required]
		[MaxLength(50)]
		public string CustomerName { get; set; }
	
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime OrderDate { get; set; }

		//must be positive
		[Range(minimum:1,maximum:double.MaxValue)]
		public double TotalAmount { get; set; }

		public virtual List<OrderItem> OrderItems { get; set; }
	}
}
