using MinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Product> products= new List<Product>()
{
	new Product(){ Id=1, ProductName="Mouse"},
	new Product(){ Id=2, ProductName="Kyeboard"}
};

app.MapGet("/products",async (HttpContext context) =>
{
	var content = string.Join("\n", products.Select(product => product.ToString()));
	await context.Response.WriteAsync(content);
});
app.MapPost("/products", async (HttpContext context, Product product) =>
{
	 products.Add(product);
	await context.Response.WriteAsync("Product Added");
	
});

//Minimal API Endpoint -GET
//app.MapGet("/", async(HttpContext context) =>
//{
//	await context.Response.WriteAsync("GET-Hello World from MinimalAPI")
//} );
//Minimal API Endpoint -POST
app.MapPost("/", async (HttpContext context) =>
{
	await context.Response.WriteAsync("POST -Hello World from MinimalAPI");
});

app.Run();
