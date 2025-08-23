using Microsoft.AspNetCore.Mvc;

namespace CRUD_Application.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
