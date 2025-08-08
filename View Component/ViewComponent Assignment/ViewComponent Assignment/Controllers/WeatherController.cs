using Microsoft.AspNetCore.Mvc;
using ViewComponent_Assignment.Models;

namespace ViewComponent_Assignment.Controllers
{
    public class WeatherController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            Citycomponent cities = new Citycomponent() {
                Cities =new List<CityWeather> {
            new CityWeather{CityUniqueCode = "LDN", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 8:00"),  TemperatureFahrenheit = 33 },
            new CityWeather{CityUniqueCode = "NYC", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 3:00"),  TemperatureFahrenheit = 60 },
            new CityWeather{CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Parse("2030-01-01 9:00"),  TemperatureFahrenheit = 82 }
                }
            };

            return View(cities);
        }

        [Route("/GetByID/{cityCode}")]
        public IActionResult GetCityByID(string cityCode)
		{
			Citycomponent cities = new Citycomponent()
			{
				Cities = new List<CityWeather> {
			new CityWeather{CityUniqueCode = "LDN", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 8:00"),  TemperatureFahrenheit = 33 },
			new CityWeather{CityUniqueCode = "NYC", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 3:00"),  TemperatureFahrenheit = 60 },
			new CityWeather{CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Parse("2030-01-01 9:00"),  TemperatureFahrenheit = 82 }
				}
			};
            foreach(var city in cities.Cities)
            {
                if (city.CityUniqueCode==cityCode)
                {
                    return View(city);
                }
                else
                {
                    return NotFound();
                }
            }
            return null;
        }

    }
}
