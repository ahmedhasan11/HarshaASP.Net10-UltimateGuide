using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;

namespace CRUD_Application.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            //storeuser registeration details into identity DB 
            return RedirectToAction(nameof(PersonsController.Index), "Persons");
        }
    }
}
