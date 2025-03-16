namespace MiddleWare.CustomMiddleWare
{
	public class MyCustomMiddleWare : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			await context.Response.WriteAsync("mycustommiddleware");
			await next(context);
		}

		//we need then add this custom middleware as a service at our program.cs //how??
		//1- using middleware.customiddleware;
		//2- builder.Services.AddTransient<MyCustomMiddleWare>();
		//3- app.UseMiddleware<MyCustomMiddleWare>();

	}

	public static class CustomMiddlewareExtension
	{

	}
}
