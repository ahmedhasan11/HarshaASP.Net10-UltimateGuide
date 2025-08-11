using Microsoft.AspNetCore.Mvc;

namespace RedirectResult_P1.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
		#region Intro To ModelBinding ##Important
		//if you remember when we want to access a value at the url the user entered 
		//we check if the context.request.query.containskey("parameter")
		//instead of the above code , what if you always check if the user entered ?bookid=10

		//so we can give this parameter to the action method
		//because the model binding will find this value in the url
		//and will assign it to the action method parameter that has the same name

		//and instead of writing context.request.query.containskey,,OR context.request.routevalues["key"]
		//we caneasily access this parameter bookid and check all the validations we need 
		#endregion


		//url: /bookstore?bookid=1
		//trying query string
		[Route("/bookstore")]
        public IActionResult Index(int bookid)
        {

            if (bookid == null)
            {
                return BadRequest();
            }

            return Content($"Book id: {bookid}", "text/plain");
		}

        //url: /bookstore/1/true
        //trying of route data
		[Route("/bookstore/{bookid}/{isloggedin}")]
		public IActionResult Index1(int bookid)
		{

			if (bookid == null)
			{
				return BadRequest();
			}

			return Content($"Book id: {bookid}", "text/plain");
		}

		/*what if you tried to send data with the 2 ways in the same time
		assume you entered this url-->bookstore/1/true/?bookid=10&isloggedin=true
		here model binding by default its priority is to take values from the route data
		so it will take the route data as the parameters in the action method,
		but if they are null
		it retrieves the query strings data inputs into the action parameters*/



		/*what if i want the model binding to take the inputs from aspecified data source
		if i want the model binding to get inputs from route only or query string only

		we have 2 atrributes that do that -->
		[fromroute]
		[fromquery]
		how they work? you put that attribute in the action parameter before each variable*/

		//[FromRoute]
		public IActionResult FromRoute([FromRoute]int bookid)
		{

			if (bookid == null)
			{
				return BadRequest();
			}

			return Content($"Book id: {bookid}", "text/plain");
		}
		//[FromQuery]
		public IActionResult FromQuery([FromQuery] int bookid)
		{

			if (bookid == null)
			{
				return BadRequest();
			}

			return Content($"Book id: {bookid}", "text/plain");
		}

		/*but in the case of using fromquery or fromroute so if the values are null in one of them
		 so modelbinding will not retrieve the values of the other one*/


		//notice that you can use [FromQuery] or [FromRoute] on a specific attribute
		//if you have a model class for example


		#region [FromBody]
		//what  if i want to send the data as xml format or JSON format or customformat
		//solution hereis the [FromBody]
		//use it in postman Body-->Raw-->then enter the data in the format you want
		//the [FromBody] will convert the data from the format you typed to the format
		//of the model object 
		//for example--> will convert JSON data into a Person object

		public IActionResult FromBody([FromBody] int bookid)
		{

			if (bookid == null)
			{
				return BadRequest();
			}

			return Content($"Book id: {bookid}", "text/plain");
		} 
		#endregion
	}
}
