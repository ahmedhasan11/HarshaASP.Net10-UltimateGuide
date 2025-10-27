using Azure;
using CRUD_Application.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

using ServiceContracts;
using ServiceContracts.DTO;

namespace CRUD_Application.Filters.ActionFilters
{
	public class PersonCreateEditPostActionFilter : IAsyncActionFilter
	{
		private readonly ICountryService _countryService;

		public PersonCreateEditPostActionFilter(ICountryService countryService)
		{
			countryService=_countryService;
		}
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (context.Controller is PersonsController personcontroller)
			{
				if (!personcontroller.ModelState.IsValid)
				{
					var personRequest=context.ActionArguments["personRequest"];
					{
						List<CountryResponse> countries = await _countryService.GetAllCountries();
						personcontroller.ViewBag.Countries = countries.Select(country => new SelectListItem() { Value = country.CountryID.ToString(), Text = country.Countryname });
						personcontroller.ViewBag.Errors =personcontroller.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
						context.Result = personcontroller.View(personRequest);
					}
				}
				else
				{
					await next();
				}
			}
			else
			{
				await next();
			}

		}
	}
}
