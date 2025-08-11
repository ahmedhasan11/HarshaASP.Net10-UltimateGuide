using e_commerce_Task_ModelBinding.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_Task_ModelBinding.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		[Route("order")]
		public IActionResult OrderGenerating(Order order)
        {

            if (ModelState.IsValid == true) 
            {
                Random random = new Random();

                int randomOrderNumber = random.Next(1,99999);

                return Content($"NewOrderNumber: {randomOrderNumber}","text/plain");

            }
            else 
            {
               string errormessages=string.Join("/n",ModelState.Values.SelectMany(err => err.Errors).Select(err2 => err2.ErrorMessage));
                return BadRequest(errormessages);
            }

        }
    }
}
