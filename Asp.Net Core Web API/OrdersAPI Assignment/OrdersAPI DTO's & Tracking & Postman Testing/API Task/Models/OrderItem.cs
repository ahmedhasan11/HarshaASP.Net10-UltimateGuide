using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Task.Models
{
	public class OrderItem
	{
		[Key]
		public Guid ItemId { get; set; }

		[Required]
		[MaxLength(50)]
		public string ProductName { get; set; }

		//must be positive
		[Range(minimum:1, maximum:int.MaxValue )]
		public int Quantity { get; set; }
		//must be positive
		[Range(minimum:1, maximum:double.MaxValue)]
		public double UnitPrice { get; set; }
		//should be calculated automatically based on the Quantity and UnitPrice
		public double TotalPrice { get; set; }

		[Required]
		public Guid OrderID { get; set;	 }

		[ForeignKey("OrderID")]
		public virtual Order order { get; set; }
	}
}
