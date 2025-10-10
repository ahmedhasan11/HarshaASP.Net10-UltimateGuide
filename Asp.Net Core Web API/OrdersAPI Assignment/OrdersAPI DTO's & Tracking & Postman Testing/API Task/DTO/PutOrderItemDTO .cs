using API_Task.Models;
using System.ComponentModel.DataAnnotations;

namespace API_Task.DTO
{
	public class PutOrderItemDTO
	{
		[Range(minimum: 1, maximum: int.MaxValue)]
		public int Quantity { get; set; }
	}
}
