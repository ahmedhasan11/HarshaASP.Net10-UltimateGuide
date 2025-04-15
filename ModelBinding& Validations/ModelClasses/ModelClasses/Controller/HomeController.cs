using Microsoft.AspNetCore.Mvc;
using ModelClasses.Models;

namespace ModelClasses.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
		[Route("bookstore/{bookid?}/{isloggedin?}")]
		public IActionResult Index([FromQuery] int? bookid, [FromRoute] bool? isloggedin
			,Book book)
		{
			if (bookid.HasValue == false)
			{
				return BadRequest("Book id is not supplied or empty");
			}

			return Content($"Book id: {bookid}", "text/plain");
		}
	}
	/*we want to get the data as a model class , so what we did that we made the parameter
	 of the action method is a model ,,, what model binding will do that it will
	create an object from this Book , and put the values of the route or query string
	into that object , and return this object data into our model parameter
	
	 so this bokk parameter recieved an object of the modelclass Class 
	
	 in brief-->the model binding will create an object of Book CLass if it 
	have a paremeter of Book type

	model binding will fill the parameter with the data it fetches from whatever
	route values or query string
	 */
}

