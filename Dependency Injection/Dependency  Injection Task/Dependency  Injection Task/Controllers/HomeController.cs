using Microsoft.AspNetCore.Mvc;
using Dependency__Injection_Task.ServiceContract;
using Dependency__Injection_Task.Services;
using Dependency__Injection_Task.Models;

namespace Dependency__Injection_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICityWeather _cityWeatherContract;

        public HomeController(ICityWeather cityWeatherContract)
        {
            _cityWeatherContract = cityWeatherContract;
        }

        [Route("/")]
        public IActionResult Index()
        {
           List<CityWeatherClass> Cities= _cityWeatherContract.GetCities();
            return View(Cities);
        }
        [Route("/weather/{cityCode}")]
        public IActionResult GetCityByID(string cityCode)
        {
           CityWeatherClass City= _cityWeatherContract.GetCityByID(cityCode);
            return View("Details",City);
        }
    }
}
