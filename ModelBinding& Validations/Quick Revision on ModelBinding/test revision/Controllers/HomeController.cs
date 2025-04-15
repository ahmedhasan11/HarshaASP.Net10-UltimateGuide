using Microsoft.AspNetCore.Mvc;
using test_revision.Models;

namespace test_revision.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromBody]Person preson)
        {
            return View();
        }
    }
}
