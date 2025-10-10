using API_Task.Models;
using System.ComponentModel.DataAnnotations;

namespace API_Task.DTO
{
	public class CreateOrderItemDTO
	{
		[Required]
		public string ProductName { get; set; }
		// existing OrderItem/Product ID in DB

		[Range(minimum:1, maximum:int.MaxValue)]
		public int Quantity { get; set; }

		[Range(minimum: 1, maximum: double.MaxValue)]
		public double UnitPrice { get; set; }
	}
}
