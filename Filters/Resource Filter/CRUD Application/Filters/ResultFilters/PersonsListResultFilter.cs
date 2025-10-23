using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUD_Application.Filters.ResultFilters
{
	public class PersonsListResultFilter : IAsyncResultFilter
	{
		private readonly ILogger<PersonsListResultFilter> _logger;
		public PersonsListResultFilter(ILogger<PersonsListResultFilter> logger)
		{
			_logger= logger;
		}
		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{      
			//before logic
			_logger.LogInformation("{FilterName}.{MethodName}-before", nameof(PersonsListResultFilter), nameof(OnResultExecutionAsync));

			await next();
			//after logic
			_logger.LogInformation("{FilterName}.{MethodName}-after", nameof(PersonsListResultFilter), nameof(OnResultExecutionAsync));
			context.HttpContext.Response.Headers["last-modified-date"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
		}
	}
}
