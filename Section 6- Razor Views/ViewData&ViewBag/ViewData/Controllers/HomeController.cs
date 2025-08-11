using Microsoft.AspNetCore.Mvc;
using ViewData.Models;

namespace ViewData.Controllers
{
    public class HomeController : Controller
    {
		#region ViewData
		/*ViewData is accessible in Controller and Views , it is used to send data from
         the controller ot the view , also used for the view to read the data controller sent
        you can easily identify the ViewData here in the controller with its key&values

        and easily go to the view and access it

        so you dont have to make complex code in the view , because its not good
        we can write our own code and data here in the controller and easily access it in the view
         */
		#endregion

		#region ViewBag
        /* ViewBag is the same as ViewData , they access the same Dictionary ,
         you can use both of them together in the same view and the same controller 
        the advanntage ViewBag take as the ViewData is that 
        in ViewBag we dont need to do the casting we did in ViewData list problem
        ViewData returns dynamic not object , ViewBag syntax is easier though
        
         you can easily access the same keys you did in ViewData*/
		#endregion
		public IActionResult Index()
        {
            ViewData["alert message"] = "this is our alert message";
            ViewBag.alert = "sadasdsaf";

            List<Person> people = new List<Person>();
            ViewData["personlist"] = people;


            return View("Index");
        }

    }
}
