using CustomValidation.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomValidation.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("register")]
        public IActionResult Index(Person person)
        {
        List<string> errorlist= ModelState.Values.SelectMany(err => err.Errors).Select(e2 => e2.ErrorMessage).ToList();

            return BadRequest(errorlist);
        }
    }
}
