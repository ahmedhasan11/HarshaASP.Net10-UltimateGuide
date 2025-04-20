using Microsoft.AspNetCore.Mvc;

namespace LocalFunctions.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Rawhtml()
        {
            return View("HtmlRaw");
        }
    }
}
