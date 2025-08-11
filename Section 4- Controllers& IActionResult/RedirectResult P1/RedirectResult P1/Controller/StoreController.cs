using Microsoft.AspNetCore.Mvc;

namespace RedirectResult_P1.Controller
{
    public class StoreController : Microsoft.AspNetCore.Mvc.Controller
    {
		[Route("/store/books")]
		public IActionResult shop()
        {
            return Content("<h1>Bookredirection</h1>", "text/html");
        }
    }
}
