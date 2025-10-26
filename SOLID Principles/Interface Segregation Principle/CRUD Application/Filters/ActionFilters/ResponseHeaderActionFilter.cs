using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUD_Application.Filters.ActionFilters
{
	public class ResponseHeaderActionFilter:IAsyncActionFilter
	{
		private readonly ILogger<ResponseHeaderActionFilter> _logger;
		private readonly string Key;
		private readonly string Value;

		public ResponseHeaderActionFilter(ILogger<ResponseHeaderActionFilter> logger, string key, string value)
		{
			_logger = logger;
			Key= key;
			Value= value;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			//structured logging
			_logger.LogInformation("{FilterName}.{MethodName}-before", nameof(ResponseHeaderActionFilter), nameof(OnActionExecutionAsync));

			await next();

			//structured logging
			_logger.LogInformation("{FilterName}.{MethodName}-after", nameof(ResponseHeaderActionFilter), nameof(OnActionExecutionAsync));
			context.HttpContext.Response.Headers[Key] = Value;
		}

		//public void OnActionExecuted(ActionExecutedContext context)
		//{
		//	//structured logging
		//	_logger.LogInformation("{FilterName}.{MethodName}",nameof(ResponseHeaderActionFilter),nameof(OnActionExecuted));
		//}

		//public void OnActionExecuting(ActionExecutingContext context)
		//{
		//	//structured logging
		//	_logger.LogInformation("{FilterName}.{MethodName}", nameof(ResponseHeaderActionFilter), nameof(OnActionExecuting));
		//	context.HttpContext.Response.Headers[Key]=Value;
		//}
	}
}
