using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;

namespace CRUD_Application.Filters.ActionFilters
{
	public class PersonsListActionFilter : IActionFilter
	{
		private readonly ILogger<PersonsListActionFilter> _logger;

		public PersonsListActionFilter(ILogger<PersonsListActionFilter> logger)
		{
			_logger = logger;
		}
		public void OnActionExecuted(ActionExecutedContext context)
		{
			//AFTER logic
			_logger.LogInformation("PersonsListActionFilter.OnActionExecuted()");
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			//BEFORE logic
			_logger.LogInformation("PersonsListActionFilter.OnActionExecuting()");

			if (context.ActionArguments.ContainsKey("searchBy"))
			{
				string? searchBy = Convert.ToString(context.ActionArguments["searchBy"]);
				if (!string.IsNullOrEmpty(searchBy))
				{
					var searchoptions = new List<string>()
					{
					nameof(PersonResponse.PersonName),
					nameof(PersonResponse.Gender),
					nameof(PersonResponse.Address),
					nameof(PersonResponse.CountryID),
					nameof(PersonResponse.DateOfBirth),
					nameof(PersonResponse.EmailAddress),
					};
					if (searchoptions.Any(temp => temp == searchBy) == false)
					{
						_logger.LogInformation("value of {searchBy}", searchBy);
						context.ActionArguments["searchBy"] = nameof(PersonResponse.PersonName);
						_logger.LogInformation("value of {searchBy}", context.ActionArguments["searchBy"]);
					}
				}
			}			
		}
	}
}
