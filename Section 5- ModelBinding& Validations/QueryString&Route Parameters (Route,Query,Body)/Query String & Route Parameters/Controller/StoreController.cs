using Microsoft.AspNetCore.Mvc;

namespace RedirectResult_P1.Controller
{
    public class StoreController : Microsoft.AspNetCore.Mvc.Controller
    {
		[Route("/store/books")]
		public IActionResult Index()
        {
            return Content("<h1>Hello world from thetest project</h1>", "text/html");
        }
    }
}
