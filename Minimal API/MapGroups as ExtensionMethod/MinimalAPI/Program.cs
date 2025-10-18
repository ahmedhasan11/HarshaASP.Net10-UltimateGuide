using MinimalAPI.Models;
using MinimalAPI.RouteGroups;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var mapgroup = app.MapGroup("/products").ProductsAPI();

//Minimal API Endpoint -GET
app.MapGet("/", async(HttpContext context) =>
{
	await context.Response.WriteAsync("GET-Hello World from MinimalAPI");
} );

//Minimal API Endpoint -POST
app.MapPost("/", async (HttpContext context) =>
{
	await context.Response.WriteAsync("POST -Hello World from MinimalAPI");
});

app.Run();
