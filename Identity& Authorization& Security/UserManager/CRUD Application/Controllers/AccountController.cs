using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceContracts.DTO;
using System.Threading.Tasks;

namespace CRUD_Application.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        //asking what is the model class for user which is ApplicationUser
        private readonly UserManager<ApplicationUser> _usermanager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _usermanager = userManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {

            if (ModelState.IsValid==false)
            {
               ViewBag.Errors= ModelState.Values.SelectMany(mv => mv.Errors)
                    .Select(temp => temp.ErrorMessage);
                return View(registerDTO);
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.Phone,
                UserName=registerDTO.Email,
                PersonName=registerDTO.PersonName                
            };

          IdentityResult result= await _usermanager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
				return RedirectToAction(nameof(PersonsController.Index), "Persons");
			}
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register",error.Description);

                }
                return View(registerDTO);
            }

			//store user registeration details into identity DB 
            //we have to create obj of this ApplicationUser class in order to create a new user




        }
    }
}
