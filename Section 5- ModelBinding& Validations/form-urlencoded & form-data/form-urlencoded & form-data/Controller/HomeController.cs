using Microsoft.AspNetCore.Mvc;
using ModelClasses.Models;

namespace ModelClasses.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
		[Route("bookstore/{bookid?}/{isloggedin?}")]
		public IActionResult Index([FromQuery] int? bookid, [FromRoute] bool? isloggedin
			, Book book)
		{
			if (bookid.HasValue == false)
			{
				return BadRequest("Book id is not supplied or empty");
			}

			return Content($"Book id: {bookid}", "text/plain");
		}
	}
	/*we have said before that modelbinding have priorities , from these was there
	 RouteValues have more priority above query string*/

	//Form Data have the most priority in model binding
	//2 types of Form Data -->1-Form-UrlEncoded  2-Form-Data

	/*Lets Discuss Form-Urlencoded --> in postman in the Request Body section
	 you will find the option called "x-www-form-urlencoded"

	the point of it is that we are sending form data to the request body,, if you remember
	that query string is working in GET , so what if we want to send the query string
	data in the Post Request?? the way here is to send the query string data 
	into the request Body

	so go to postman and in Body select the option of form-urlencoded
	and here you can put the query string parameters and values and send your request
	 */
}

