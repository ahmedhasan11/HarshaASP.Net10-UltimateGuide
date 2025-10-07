using API_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Task.Context
{
	public class OrdersContext:DbContext
	{
		public OrdersContext(DbContextOptions options):base(options)
		{
		}
		public OrdersContext() { }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>().HasData(new Order()
			{
				OrderID = Guid.Parse("{2ED6362C-0837-4078-AD22-EDE5E1066DD7}"),
				CustomerName = "Ayman",
				OrderDate = new DateTime(2025, 3, 5),
				OrderNumber = "Order_2025_2",
				TotalAmount = 7000
			}, new Order()
			{
				OrderID = Guid.Parse("{A68D26E4-40FB-4644-8D4C-4C1C42376202}"),
				CustomerName = "Ahmed",
				OrderDate = new DateTime(2025, 1, 15),
				OrderNumber = "Order_2025_1",
				TotalAmount = 9000
			}
			);
			modelBuilder.Entity<OrderItem>().HasData(new OrderItem()
			{
				ItemId = Guid.Parse("{826B5B2C-1F66-44F6-8960-7DD4DE1FEBEB}"),
				ProductName = "Jeans",
				Quantity = 20,
				UnitPrice = 30.00,
				TotalPrice = 600,
				OrderID = Guid.Parse("{A68D26E4-40FB-4644-8D4C-4C1C42376202}")
			}, new OrderItem()
			{
				ItemId = Guid.Parse("{09531768-233D-4484-9552-C799C8C65534}"),
				ProductName = "Jeans",
				Quantity = 10,
				UnitPrice = 20.00,
				TotalPrice = 200,
				OrderID = Guid.Parse("{2ED6362C-0837-4078-AD22-EDE5E1066DD7}")
			}
			);
			base.OnModelCreating(modelBuilder);
		}

	}
}
