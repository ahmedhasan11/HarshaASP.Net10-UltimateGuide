using Microsoft.AspNetCore.Mvc;

namespace CRUD_Application.Areas.Admin.Controllers
{
    [Area("Admin")] //to specify that this area is related to Admin
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
