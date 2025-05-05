using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RazorViews_Task.Models;

namespace RazorViews_Task.Controllers
{
    public class HomeController : Controller
    {
		public List<CityWeather> ListOfCities = new List<CityWeather>()
			{
			new CityWeather(){CityUniqueCode = "LDN", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 8:00"),TemperatureFahrenheit = 33},
			new CityWeather(){CityUniqueCode = "NYC",   CityName = "London",DateAndTime = DateTime.Parse("2030-01-01 3:00"),TemperatureFahrenheit = 60},
			new CityWeather(){CityUniqueCode = "PAR",   CityName = "Paris", DateAndTime = DateTime.Parse("2030-01-01 9:00"),TemperatureFahrenheit = 82}
			};
		public IActionResult Index()
        {
            return View();
        }


        [Route("/")]
		public IActionResult Details()
		{
			return View("Details", ListOfCities);
		}
		[Route("/weather/{CityCode:string?}")]
		public IActionResult Details_withparameter(string CityCode)
		{
			if (string.IsNullOrEmpty(CityCode))
			{
				return BadRequest("no city code is supplied");
			}

			CityWeather model= ListOfCities.Where(s => s.CityUniqueCode == CityCode).FirstOrDefault();
			//foreach (var city in ListOfCities)
			//{
			//	if (CityCode == city.CityUniqueCode)
			//	{
			//		return View("Details_withparameter", city);
			//	}
			//	else
			//	{
			//		return BadRequest("city code is not right");
			//	}
			//}
			return View("Details-withparameter", model);
		}


	}
}
