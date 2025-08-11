using Microsoft.AspNetCore.Mvc;
using Model_Validation.Models;

namespace Model_Validation.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
		[Route("register")]
		public IActionResult Index(Person person)
		{
			List<string> errorlist =ModelState.Values.SelectMany(err => err.Errors).Select(e2 => e2.ErrorMessage).ToList();

			return BadRequest(errorlist);
			//what if you want to print a custom message of a specific validation of specific attribute
			//on the attribute of [Required] you can make this [Required(ErrorMessage="custom msg")]


			//if (ModelState.IsValid==false)
			//{
			//	foreach (var item in ModelState.Values)
			//	{
			//		foreach (var error in item.Errors)
			//		{
			//			errorlist.Add(error.ErrorMessage);
			//		}
			//	}
			//	string errors = string.Join("/n", errorlist);
			//	return BadRequest();
			//}
			
			return Content($"{person}");
		}
		/*all Model Validation errors are stored in an obj or property  
		 called: ModelState , this ModelState contains 3 properties
		1-ModelState.IsValid--> true if novalidation errors
		2-ModelState.Values-->contains each model property value 
		with corresponding validation error property called "Errors" that 
		contains list of valdiation erorrs for that model property*/

	}
}
