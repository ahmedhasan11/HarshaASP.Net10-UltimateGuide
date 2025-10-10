using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Task.Context;
using API_Task.Models;
using API_Task.DTO;

namespace API_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
		private readonly OrdersContext _db;
		public OrdersController(OrdersContext db)
		{
			_db = db;
		}

		[HttpGet]
		public async Task<ActionResult<List<Order>>> GetOrders()
		{
			//GET-- > Retrieve all orders
			var Orders = await _db.Orders.Include(o=>o.OrderItems).ToListAsync();

			return Orders;
		} //tested
		[HttpGet("{orderID}")]
		public async Task<ActionResult<Order>> GetOrderByID(Guid orderID)
		{
			//if (orderID==Guid.Empty)
			//{
			//	//return BadRequest();
			//	return Problem(detail:"OrderID can't be null", title:"Empty OrderID", statusCode:);
			//}
			var order = await _db.Orders.Include(o=>o.OrderItems).FirstOrDefaultAsync(o=>o.OrderID==orderID);
			if (order == null)
			{
				//return NotFound();
				return Problem(detail: "can't find Order with specified ID", title: "Invalid OrderID", statusCode: 404);
			}
			return order;
		} //tested
		[HttpPost]
		public async Task<IActionResult> PostOrder(CreateOrderDTO createOrderDTO)
		{
			var ordercountinyear = await _db.Orders.Where(temp => temp.OrderDate.Year == DateTime.Now.Year).CountAsync();
			Order order= new Order();
			order.OrderID = Guid.NewGuid();
			order.CustomerName=createOrderDTO.CustomerName;
			order.OrderDate = DateTime.Now;
			order.OrderNumber = $"Orders_{DateTime.Now.Year}_{ordercountinyear + 1}";
			order.TotalAmount = 0;


			foreach (var orderdtoitem in createOrderDTO.OrderItems)
			{
				var orderitem = new OrderItem()
				{
					ItemId= Guid.NewGuid(),
					ProductName = orderdtoitem.ProductName,
					Quantity = orderdtoitem.Quantity,
					UnitPrice= orderdtoitem.UnitPrice,
					OrderID= order.OrderID,
					TotalPrice= orderdtoitem.Quantity * orderdtoitem.UnitPrice
				};
				order.TotalAmount += orderitem.TotalPrice;
				order.OrderItems.Add(orderitem);
			}

			_db.Orders.Add(order);
			await _db.SaveChangesAsync();

			return CreatedAtAction("GetOrderByID", new { orderID = order.OrderID }, order);

		} //tested
		[HttpPut("{orderID}")]

		public async Task<IActionResult> PutOrder(Guid orderID, PutOrderDTO putOrderDTO)
		{
			if (orderID != putOrderDTO.OrderID)
			{
				return Problem(detail: "OrderID is not the same as route", title: "Conflicts in id", statusCode: 400);
			}

			// Load order with its items
			var existingOrder = await _db.Orders
				.Include(o => o.OrderItems)
				.AsNoTracking() // 🚀 no EF tracking!
				.FirstOrDefaultAsync(o => o.OrderID == orderID);

			if (existingOrder == null)
			{
				return Problem(detail: "Can't find Order with specified ID", title: "Invalid OrderID", statusCode: 404);
			}

			// 🔥 Delete all old items directly
			var oldItems = _db.OrderItems.Where(i => i.OrderID == orderID);
			_db.OrderItems.RemoveRange(oldItems);
			await _db.SaveChangesAsync(); // make sure they’re gone

			// 🧱 Recreate the order object cleanly
			existingOrder.TotalAmount = 0;
			existingOrder.OrderItems = new List<OrderItem>();

			foreach (var dto in putOrderDTO.OrderItems)
			{
				var item = new OrderItem
				{
					ItemId = Guid.NewGuid(),
					ProductName = dto.ProductName,
					Quantity = dto.Quantity,
					UnitPrice = dto.UnitPrice,
					OrderID = existingOrder.OrderID,
					TotalPrice = dto.Quantity * dto.UnitPrice
				};

				existingOrder.TotalAmount += item.TotalPrice;
				existingOrder.OrderItems.Add(item);
			}

			// 🚀 Now attach the clean order and mark as Modified
			_db.Attach(existingOrder);
			_db.Entry(existingOrder).State = EntityState.Modified;

			foreach (var item in existingOrder.OrderItems)
			{
				_db.Entry(item).State = EntityState.Added;
			}

			await _db.SaveChangesAsync();
			return NoContent();
		} //tested ,,tracking problems

		[HttpDelete("{orderID}")]
		public async Task<IActionResult> DeleteOrder(Guid orderID)
		{
			var order = await _db.Orders.FindAsync(orderID);
			if (order == null)
			{
				return Problem(detail: "can't find Order with specified ID", title: "Invalid OrderID", statusCode: 404);
			}
			_db.Remove(order);
			await _db.SaveChangesAsync();
			return NoContent();
		} //tested

		[HttpGet("{orderID}/items")]
		public async Task<ActionResult<List<OrderItem>>> GetOrderItems(Guid orderID)
		{
			var order = await _db.Orders.Include(o=>o.OrderItems).FirstOrDefaultAsync(o=>o.OrderID==orderID);
			if (order==null)
			{
				return Problem(detail:"there is no order with this id", title:"Invalid orderID", statusCode:404);
			}

			List<OrderItem> orderitems= order.OrderItems;
			return orderitems;
		} //tested

		[HttpGet("{orderID}/Items/{orderitemID}")]
		public async Task<ActionResult<OrderItem>> GetSpecificOrderItem(Guid orderID, Guid orderitemID)
		{
			var order = await _db.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o=>o.OrderID==orderID);
			if (order==null)
			{
				return Problem(detail:"no order found with this ID", title: "Invalid orderID", statusCode: 404);
			}

			var orderitem = order.OrderItems.FirstOrDefault(orderitem => orderitem.ItemId == orderitemID);
			if (orderitem==null)
			{
				return Problem(detail:"there is no orderitem with this id in this order",title:"orderitem is not in this order",statusCode:404);
			}

			return orderitem;
		} //tested

		[HttpPost("{orderID}/Items")]
		public async Task<IActionResult> PostOrderItemIntoOrder(Guid orderID, CreateOrderItemDTO dto)
		{
			var order = await _db.Orders
				.Include(o => o.OrderItems)
				.FirstOrDefaultAsync(o => o.OrderID == orderID);

			if (order == null)
				return NotFound("Order not found.");

			var orderItem = new OrderItem
			{
				ItemId = Guid.NewGuid(),
				ProductName = dto.ProductName,
				Quantity = dto.Quantity,
				UnitPrice = dto.UnitPrice,
				TotalPrice = dto.Quantity * dto.UnitPrice,
				OrderID = order.OrderID
			};

			// Insert first
			_db.OrderItems.Add(orderItem);
			await _db.SaveChangesAsync();

			// Update order total separately
			order.TotalAmount += orderItem.TotalPrice;
			_db.Entry(order).State = EntityState.Modified;//“Hey, this order already exists in the database — I changed something on it, so go update it.”
			await _db.SaveChangesAsync();

			return CreatedAtAction("GetOrderByID", new { orderID = order.OrderID }, order);
		} //tested ,,tracking problems


		[HttpPut("{orderID}/Items/{itemID}")]
		public async Task<IActionResult> PutExisitngOrderItemThatIsInsideOrder
			(Guid orderID, Guid itemID,PutOrderItemDTO putOrderItemDTO)
		{
			var order = await _db.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderID == orderID);
			if (order==null)
			{
				return Problem(detail: "there is no Order with this Order ID", title:"Invalid Order ID",statusCode:404);
			}
			var orderitemfromDB = await _db.OrderItems.FindAsync(itemID);
			if (orderitemfromDB == null)
			{
				return Problem(detail: "there is no OrderItem with this OrderItem ID", title: "Invalid OrderItem ID", statusCode:404);
			}
			if (orderitemfromDB.OrderID!=order.OrderID)
			{
				return Problem(detail: "this OrderItem not belong to this Order", title: "OrderItem is not in this Order", statusCode:400);
			}
			// Subtract old total of this item first
			order.TotalAmount -= orderitemfromDB.TotalPrice;
			orderitemfromDB.Quantity = putOrderItemDTO.Quantity;
			orderitemfromDB.TotalPrice = orderitemfromDB.Quantity * orderitemfromDB.UnitPrice;
			// Add the new total of this item
			order.TotalAmount += orderitemfromDB.TotalPrice;

			
			await _db.SaveChangesAsync();

			return NoContent();


		} //tested 

		[HttpDelete("{orderID}/Items/{itemID}")]
		public async Task<IActionResult> DeleteOrderItemFromSpecificOrder(Guid orderID, Guid itemID)
		{
			var order = await _db.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderID == orderID);
			if (order==null)
			{
				return Problem(detail: "No order found with the given ID.", title: "Invalid Order ID", statusCode:404);
			}
			var orderitem = await _db.OrderItems.FindAsync(itemID);
			if (orderitem==null)
			{
				return Problem(detail: "No order item found with the given ID.", title: "Invalid OrderItem ID", statusCode: 404);
			}

			if (orderitem.OrderID!=order.OrderID)
			{
				return Problem(detail: "This order item does not belong to the specified order.", title: "OrderItem Mismatch", statusCode: 400);
			}

			order.TotalAmount -= orderitem.TotalPrice;
			order.OrderItems.Remove(orderitem);
			await _db.SaveChangesAsync();
			return NoContent();
		} //tested
	}
}

