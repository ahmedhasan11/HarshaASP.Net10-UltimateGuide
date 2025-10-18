using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Models;

namespace MinimalAPI.RouteGroups
{
	//we made it statis because we want to create an extension method
	public static class ProductsMapGroup
	{
		private static List<Product> products = new List<Product>()
			{
				new Product(){ Id=1, ProductName="Mouse"},
				new Product(){ Id=2, ProductName="Kyeboard"}
			};
		public static RouteGroupBuilder ProductsAPI(this RouteGroupBuilder group)
		{
			//GET Products -->Minimal API
			group.MapGet("/", async (HttpContext context) =>
			{
				var content = string.Join("\n", products.Select(product => product.ToString()));
				await context.Response.WriteAsync(content);
			});

			//Route& RouteParameters in Minimal API
			group.MapGet("/{id:int}", async (HttpContext context, int id) =>
			{
				Product? product = products.FirstOrDefault(p => p.Id == id);
				if (product == null)
				{
					context.Response.StatusCode = 400;
					await context.Response.WriteAsync("Invalid ID");
					return;
				}
				await context.Response.WriteAsync(product.ToString());
			});

			//POST Product -->Minimal API
			group.MapPost("/", async (HttpContext context, Product product) =>
			{
				products.Add(product);
				await context.Response.WriteAsync("Product Added");

			});

			//PUT Product -->Minimal API
			group.MapPut("/{id:int}", async (HttpContext context,[FromBody] Product productfromquery,
				int id) =>
			{
				Product? product = products.FirstOrDefault(p => p.Id == id);
				if (product==null)
				{
					context.Response.StatusCode = 400;//bad request
					await context.Response.WriteAsync("Invalid id");
					return;
				}
				product.ProductName= productfromquery.ProductName;
				await context.Response.WriteAsync("product updated");
			});

			group.MapDelete("/{id}", async (HttpContext context, int id) =>
			{
				Product? product = products.FirstOrDefault(p => p.Id == id);
				if (product==null)
				{
					context.Response.StatusCode = 400;//bad request
					await context.Response.WriteAsync("Invalid id");
					return;
				}
				products.Remove(product);
				await context.Response.WriteAsync("product deleted");

			});
			return group;
		}

	}
}
