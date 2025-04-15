using CustomModelBinder.ModelBinderCustomClass;
using CustomModelBinder.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomModelBinder.Controller
{

    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
	{

    [Route("register")]
		//Example JSON: { "PersonName": "William", "Email": "william@example.com", "Phone": "123456", "Password": "william123", "ConfirmPassword": "william123" }
		public IActionResult Index([FromBody][ModelBinder(BinderType = typeof(PersonModelBinder))] Person person)
		{
			if (!ModelState.IsValid)
			{
				//get error messages from model state
				string errors = string.Join("\n", ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage));

				return BadRequest(errors);
			}

			return Content($"{person}");
		}
	}
}

