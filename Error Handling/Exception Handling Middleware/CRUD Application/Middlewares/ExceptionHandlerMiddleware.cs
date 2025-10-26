using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRUD_Application.Middlewares
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlerMiddleware> _logger;

		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext); //calling the subsequent middleware
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					_logger.LogError("{ExceptionType}.{ExceptionMessage}",
					ex.InnerException.GetType().ToString(), ex.InnerException.Message);
				}
				else
				{
					_logger.LogError("{ExceptionType}.{ExceptionMessage}",
					ex.GetType().ToString(), ex.Message);
				}
				httpContext.Response.StatusCode = 500;
				await httpContext.Response.WriteAsync("error occured");


			}
		}
	}
		// Extension method used to add the middleware to the HTTP request pipeline.
		public static class ExceptionHandlerMiddlewareExtensions
		{
			public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
			{
				return builder.UseMiddleware<ExceptionHandlerMiddleware>();
			}
		}
	
}
