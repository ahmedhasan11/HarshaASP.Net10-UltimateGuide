using CustomValidation.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomValidation.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
		//what  if i want to send the data as xml format or JSON format or customformat
		//solution hereis the [FromBody]
		//use it in postman Body-->Raw-->then enter the data in the format you want
		//the [FromBody] will convert the data from the format you typed to the format
		//of the model object 
		//for example--> will convert JSON data into a Person object
		[Route("register")]
        public IActionResult Index([FromBody]Person person)
        {
        List<string> errorlist= ModelState.Values.SelectMany(err => err.Errors).Select(e2 => e2.ErrorMessage).ToList();

            return BadRequest(errorlist);
        }


	}
}
