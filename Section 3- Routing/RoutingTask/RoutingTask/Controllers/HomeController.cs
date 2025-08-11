using Microsoft.AspNetCore.Mvc;

namespace RoutingTask.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/countries")]
        public IActionResult Countries()
        {

            return View();
        }
    }
}
