using Microsoft.AspNetCore.Mvc;

namespace Environments.Controllers
{
    public class HomeController : Controller
    {
        //inject the environment service to access it inthe controller
        private readonly IWebHostEnvironment _environment;
        public HomeController(IWebHostEnvironment env)
        {
            _environment = env;
        }
        public IActionResult Index()
        {
            //use this to show which Environment the application is in
            ViewBag.EnvironmentName = _environment.EnvironmentName;

            return View();

        }
    }
}
