using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUD_Application.Filters.ActionFilters
{
	public class ResponseHeaderActionFilter:IActionFilter
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

		public void OnActionExecuted(ActionExecutedContext context)
		{
			//structured logging
			_logger.LogInformation("{FilterName}.{MethodName}",nameof(ResponseHeaderActionFilter),nameof(OnActionExecuted));
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			//structured logging
			_logger.LogInformation("{FilterName}.{MethodName}", nameof(ResponseHeaderActionFilter), nameof(OnActionExecuting));
			context.HttpContext.Response.Headers[Key]=Value;
		}
	}
}
