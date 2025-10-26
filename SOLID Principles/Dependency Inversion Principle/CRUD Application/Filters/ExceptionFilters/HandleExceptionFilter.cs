using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUD_Application.Filters.ExceptionFilters
{
	public class HandleExceptionFilter : IExceptionFilter
	{
		private readonly ILogger<HandleExceptionFilter> _logger;
		private readonly IHostEnvironment _hostEnvironment;
		public HandleExceptionFilter(ILogger<HandleExceptionFilter> logger, IHostEnvironment hostEnvironment)
		{
			_logger = logger;
			_hostEnvironment = hostEnvironment;
		}
		public void OnException(ExceptionContext context)
		{
			_logger.LogError
				("ExceptionFilter {FilterName}.{MethodName}\n{ExceptionType}\n{ExceptionMessage}",
				nameof(HandleExceptionFilter),nameof(OnException),
				context.Exception.GetType().ToString(),nameof(context.Exception.Message));

			//be careful , the specific error message should be shown only if you
			//are in development , so end user shouldn't see these errors
			//ShortCircuting and giving our error message details to be shown 
			if (_hostEnvironment.IsDevelopment())
			{
				context.Result = new ContentResult()
				{
					Content = context.Exception.Message,
					StatusCode = 500
				};
				//now if the exception happens in production or staging
				//, HTTP error 500 is returned 
			};


		}
	}
}
