using Microsoft.AspNetCore.Mvc;

namespace ModelClasses.Controller
{
    public class StoreController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
