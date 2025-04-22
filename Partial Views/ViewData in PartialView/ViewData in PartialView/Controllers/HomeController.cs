using Microsoft.AspNetCore.Mvc;

namespace ViewDatainPartialView.Controllers
{
    public class HomeController : Controller
    {
			[Route("/")]
			public IActionResult Index()
			{
			/*now we are trying to send these data to our View , we can access these
			 throughViewBag or ViewData either in View or PartialView that you
			invoked in your View*/

			/*but what if you made an edit at this data on your View?? of course
			 the PartialView will get the updated data that the view have edited*/

			/*IMPORTANTTTTTTTT in your View
			 what if you invoked the PartialView 2 times in your View
			first one: <partial name=""></partial>
			after it you made your code block of the edited version of data
			then invoking the second one:<partial name=""></partial>
			
			 output: first prints the old version
			then prints the edited version because in the first version data was still
			not updated , the partialview didnt see the update*/

			/*what if you edited the data on the partial view??
			 lets say you edited the ViewBag.ListTitle and then you invoked the
			@ViewBag.ListTitle this will be shown as the updated data you did on
			the partial views , but this will not be considered as an update 
			or edit on the actual data on the View
			
			 in brief:changes in partial views on data doesnt affect the data 
			on the View*/

				ViewData["ListTitle"] = "Cities";
				ViewData["ListOfCountries"] = new List<string>
				{
					"paris",
					"newyork",
					"newmumbai",
					"rome"
				};
				return View();
			}

			[Route("about")]
			public IActionResult About()
			{
				return View();
			}
		}
	}

