using Microsoft.AspNetCore.Mvc;
using PartialViewResult.Models;

namespace PartialViewResult.Controllers
{
    public class HomeController : Controller
    {
			[Route("/")]
			public IActionResult Index()
			{
				return View();
			}

			[Route("about")]
			public IActionResult About()
			{
				return View();
			}

		//we will invoke this actionmethod through javascript code asynchronously
		[Route("async-partial")]
			public IActionResult asyncPartial()
			{
			ListPartialView modobjpartial = new ListPartialView()
			{
				ListTitle="blalbaklabalabl",
				ListOfCountries = {"python","c#","Go"}
			};
			//you can return only the Partial View name , or return the model with it
			return PartialView("_ListPartialView", modobjpartial);
			}

		}
	}

