using Creating_ViewComponent.Models;
using Microsoft.AspNetCore.Mvc;

namespace Creating_ViewComponent.Controllers
{
    public class HomeController : Controller
    {
        [Route ("/")]
        public IActionResult Index()
        {
            return View();
        }
		[Route("/About")]
		public IActionResult About()
		{
			return View();
		}
        [Route("/load-Persons")]
        public IActionResult LoadDataJS()
        {
			PersonGridModel model = new PersonGridModel()
			{
				GridTitle = "Grid1",
				Persons = new List<Person>(){ new Person { PersonName="ahmed", JobTitle="backend" },
				new Person{ PersonName="mohamed", JobTitle="frontend"},
				new Person{PersonName="ayman", JobTitle="cloud" } }

			};

			return ViewComponent("Grid", new { grid=model});
        }
	}
}
