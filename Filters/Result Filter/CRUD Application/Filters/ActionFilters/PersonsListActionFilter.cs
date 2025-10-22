using CRUD_Application.Controllers;
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
			//normal logging
			//_logger.LogInformation("PersonsListActionFilter.OnActionExecuted()"); //instead do next code
			//Structured Logging-->
			_logger.LogInformation("{FilterName}.{MethodName}", nameof(PersonsListActionFilter), nameof(OnActionExecuted));

			PersonsController personsController = (PersonsController)context.Controller;

			IDictionary<string, object?>? parameters = (IDictionary<string, object?>?)context.HttpContext.Items["arguments"];
			if (parameters!=null)
			{
				if (parameters.ContainsKey("CurrentsearchBy"))
				{
					personsController.ViewData["CurrentsearchBy"] = Convert.ToString(parameters["CurrentsearchBy"]);
				}
				if (parameters.ContainsKey("CurrentsearchString") )
				{
					personsController.ViewData["CurrentsearchString"] = Convert.ToString(parameters["CurrentsearchString"]);
				}
				if (parameters.ContainsKey("CurrentsortBy") )
				{
					personsController.ViewData["CurrentsortBy"] = Convert.ToString(parameters["CurrentsortBy"]);
				}
				if (parameters.ContainsKey("CurrentsortOrder") )
				{
					personsController.ViewData["CurrentsortOrder"] = Convert.ToString(parameters["CurrentsortOrder"]);
				}

			}
			personsController.ViewBag.SearchFields = new Dictionary<string, string>()
			{
                //right Hand Side-->which wil be displayed to the user
                { nameof(PersonResponse.PersonName), "Person Name" },
				{ nameof(PersonResponse.EmailAddress), "Email" },
				{ nameof(PersonResponse.DateOfBirth), "Date Of Birth" },
				{ nameof(PersonResponse.Gender), "Gender" },
				{ nameof(PersonResponse.CountryID), "Country" },
				{ nameof(PersonResponse.Address), "Address" },
			};

		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			//provide the arguments to OnActionExecuted
			context.HttpContext.Items["arguments"] = context.ActionArguments;
			//BEFORE logic
			//Structured Logging-->
			_logger.LogInformation("{FilterName}.{MethodName}", nameof(PersonsListActionFilter), nameof(OnActionExecuting));

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
