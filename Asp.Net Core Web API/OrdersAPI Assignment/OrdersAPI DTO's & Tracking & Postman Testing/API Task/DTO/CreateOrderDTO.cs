using API_Task.Models;
using System.ComponentModel.DataAnnotations;

namespace API_Task.DTO
{
	public class CreateOrderDTO
	{
		[Required]
		public string CustomerName { get; set; }
		[Required]
		public List<CreateOrderItemDTO> OrderItems { get; set; }
	}
}
