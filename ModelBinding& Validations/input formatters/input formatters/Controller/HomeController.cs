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
		//input formatters :inernal classes used to transform or convert the request body
		//info into a model object 
		//modelbinding know which input format to use by
		//the Content-Type in the request header
		//Content-Type:application/JSON-->modelbinding use JSON input format
		//Content-Type:application/XML-->modelbinding use XML input format

		//by default controller in mvc have only the JSON input format
		//so if you want to use the XML you should import it in the program.cs
		//builder.Services.AddControllers().AddXmlSerializerFormatters();

		//postman automatically when you select the type of data in body-->raw
		//if you selected XML so postman make a request header Content-Type:application/XML
		//same as JSON of course


	}
}
