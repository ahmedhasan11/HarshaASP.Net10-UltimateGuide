using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUD_Application.Filters.ResourceFilters
{
	public class CreateDisableResourceFilter:IAsyncResourceFilter
	{
		private readonly ILogger<CreateDisableResourceFilter> _logger;
		private readonly bool _IsDisabled;
		public CreateDisableResourceFilter(ILogger<CreateDisableResourceFilter> logger, bool IsDisabled=true)
		{
		_logger = logger;
			_IsDisabled = IsDisabled;
		}

		public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
		{
			_logger.LogInformation("{FilterName}.{MethodName}-Before", nameof(CreateDisableResourceFilter),nameof(OnResourceExecutionAsync));
			if (_IsDisabled)
			{
				context.Result = new StatusCodeResult(501); //501 is better for
				//that you are telling the user that this button temp disabled
			}
			else
			{
				await next();
			}
			_logger.LogInformation("{FilterName}.{MethodName}-Before", nameof(CreateDisableResourceFilter), nameof(OnResourceExecutionAsync));
		}
	}
}
