using System.ComponentModel.DataAnnotations;

namespace API_Task.DTO
{
	public class PutOrderDTO
	{
		[Required]
		public Guid OrderID { get; set; }
		public List<CreateOrderItemDTO> OrderItems { get; set; }
	}
}
