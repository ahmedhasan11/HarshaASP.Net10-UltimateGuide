using Microsoft.AspNetCore.Mvc;

namespace RedirectResult_P1.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        
        [Route("/booksstore")]
        public IActionResult book()
        {
            //we made anonymous obj because the method need a route values to be given
            //sowemade a dummy object

            // return new RedirectToActionResult("shop","Store", new { }); //this make 302 status code (moved temporarily)
            //shortcut :
            //return RedirectToAction("shop","Store", new { });-->302


            //return new RedirectToActionResult("shop", "Store", new { }, permanent:true); //this make 301 (moved permanent)
            //shortcut:
            return RedirectToActionPermanent("shop", "Store", new { });

		}
    }
}
