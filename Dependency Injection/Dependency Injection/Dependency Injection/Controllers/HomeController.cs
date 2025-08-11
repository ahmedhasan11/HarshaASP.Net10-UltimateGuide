using Microsoft.AspNetCore.Mvc;
using ServiceContract;
using Services;
namespace Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {
        //  private readonly Service service;
        private readonly ICityService _cityService;
        public HomeController(ICityService cityservice )
            //cityservice now contains the  object of the city service
        {
            // service = new Service(); //Direct Dependency (the Problem)
            //the problem here is the Controller is Fully Dependent on Service
            //you have to do object from the service to use methods 

            _cityService = cityservice;

		}
		public IActionResult Index()
        {
           List<string>cities= _cityService.GetCountries();
            return View(cities);
        }
        //method injection



		//public IActionResult About([FromServices] ICityService _cityService)
		//{
  //          _cityService.GetCountries();
		//	return View();
		//}
	}
}
