using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceContracts.DTO;
using System.Threading.Tasks;

namespace CRUD_Application.Controllers
{
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        //asking what is the model class for user which is ApplicationUser
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _usermanager = userManager;
            _signinManager = signInManager;
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
                //weneed here to make the login
               await _signinManager.SignInAsync(user, isPersistent: true);
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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> Login(LoginDTO loginDTO, string? ReturnUrl)
		{
            if (!ModelState.IsValid)
            {
				ViewBag.Errors = ModelState.Values.SelectMany(mv => mv.Errors)
					 .Select(temp => temp.ErrorMessage);
				return View(loginDTO);
			}
            //here
           var result= await _signinManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false,lockoutOnFailure:false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(ReturnUrl)&& Url.IsLocalUrl(ReturnUrl))
                {
                    return LocalRedirect(ReturnUrl);
                }
                return RedirectToAction(nameof(PersonsController.Index), "Persons");

            }
            else
            {
                ModelState.AddModelError("Login", "Invalid Email or Password");
                return View();
            }


		}

        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();//removes the identity cookie of theuserthat is 
            //already logged in 
            return RedirectToAction(nameof(PersonsController.Index), "Persons");
        }

        public async Task<IActionResult> EmailValidation(string email)
        {
            ApplicationUser? user =await _usermanager.FindByEmailAsync(email);
            if (user==null)
            {
                return Json(true);//valid
            }
            else
            {
                return Json(false);//invalid
            }
        }
	}
}
