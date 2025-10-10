using API_Task.Context;
using API_Task.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace API_Task.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly OrdersContext _db;
		public TestController(OrdersContext db)
		{
			_db = db;
		}

		[HttpGet]
		public async Task<ActionResult<List<Order>>> GetOrders()
		{
			//GET-- > Retrieve all orders
			var Orders = await _db.Orders.ToListAsync();

			return Orders;
		}
		[HttpGet("{orderID}")]
		public async Task<ActionResult<Order>> GetOrderByID(Guid orderID)
		{
			//if (orderID==Guid.Empty)
			//{
			//	//return BadRequest();
			//	return Problem(detail:"OrderID can't be null", title:"Empty OrderID", statusCode:);
			//}
			var order = await _db.Orders.FindAsync(orderID);
			if (order==null)
			{
				//return NotFound();
				return Problem(detail:"can't find Order with specified ID", title:"Invalid OrderID", statusCode:404);
			}
			return order;
		}
		[HttpPost]
		public async Task<IActionResult> PostOrder([Bind(nameof(Order.CustomerName),nameof(Order.OrderItems))]Order order)
		{
			var ordercountinyear = await _db.Orders.Where(temp => temp.OrderDate.Year == DateTime.Now.Year).CountAsync();
			order.OrderID = Guid.NewGuid();
			order.OrderNumber = $"Orders_{DateTime.Now.Year}_{ordercountinyear + 1}";
			order.OrderDate = DateTime.Now;
			order.TotalAmount = 0;
			foreach (OrderItem orderItem in order.OrderItems)
			{
				orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
				order.TotalAmount += orderItem.TotalPrice;
			}
			_db.Orders.Add(order);
			await _db.SaveChangesAsync();

			return CreatedAtAction("GetOrderByID",new { orderID=order.OrderID},order);
			
		}
		[HttpPut("{orderID}")]
		public async Task<IActionResult> PutOrder(Guid id,[Bind(nameof(Order.OrderItems))] Order order )
		{
			if (id!=order.OrderID)
			{
				return Problem(detail: "OrderID is not the same as route", title: "Conflicts in id", statusCode: 400);
			}

			var MainOrder=await _db.Orders.FindAsync(id);
			if (MainOrder==null)
			{
				return Problem(detail: "can't find Order with specified ID", title: "Invalid OrderID", statusCode: 404);
			}

			MainOrder.OrderItems.Clear();
			MainOrder.TotalAmount = 0;
			foreach (OrderItem orderItem in order.OrderItems)
			{
				MainOrder.OrderItems.Add(orderItem);
				orderItem.TotalPrice= orderItem.Quantity* orderItem.UnitPrice;
				MainOrder.TotalAmount+= orderItem.TotalPrice;

			}

			await _db.SaveChangesAsync();
			return NoContent();

		}
		[HttpDelete("{orderID}")]
		public async Task<IActionResult> DeleteOrder(Guid orderID)
		{
			var order= await _db.Orders.FindAsync(orderID);
			if (order==null)
			{
				return Problem(detail: "can't find Order with specified ID", title: "Invalid OrderID", statusCode: 404);
			}
			_db.Remove(order);
			await _db.SaveChangesAsync();
			return NoContent();
		}


	}
}
