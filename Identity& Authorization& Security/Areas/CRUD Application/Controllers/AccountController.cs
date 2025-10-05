using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
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
        private readonly RoleManager<ApplicationRole> _roleManagaer;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManagaer)
        {
            _usermanager = userManager;
            _signinManager = signInManager;
            _roleManagaer = roleManagaer;
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
                //we have tosee if a user who is tryingto register and pressed admin checkbox, we still donthave Admin Role 
                //so wehave to make it first
                if (registerDTO.Role == ApplicationRoleEnum.Admin)
                {
					if (await _roleManagaer.FindByNameAsync(ApplicationRoleEnum.Admin.ToString()) is null)
					{
						//we have tosee if a user who is tryingto register and pressed admin checkbox, we still donthave Admin Role 
						//so wehave to make it first
						ApplicationRole AdminRole = new ApplicationRole() { Name = ApplicationRoleEnum.Admin.ToString() };
						await _roleManagaer.CreateAsync(AdminRole);


					}
                    //add the newuserintoadmin role
					await _usermanager.AddToRoleAsync(user, ApplicationRoleEnum.Admin.ToString());
				}
                else
                {
					//we have tosee if a user who is tryingto register and pressed User checkbox, we still donthave User Role 
					//so wehave to make it first
					await _usermanager.AddToRoleAsync(user, ApplicationRoleEnum.User.ToString());
				}



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
                ApplicationUser user = await _usermanager.FindByEmailAsync(loginDTO.Email);
                if (user!=null)
                {
					if (await _usermanager.IsInRoleAsync(user, ApplicationRoleEnum.Admin.ToString()))
					{
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
					}
					else
					{
						if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
						{
							return LocalRedirect(ReturnUrl);
						}
						return RedirectToAction(nameof(PersonsController.Index), "Persons");
					}
				}


			}

                ModelState.AddModelError("Login", "Invalid Email or Password");
                return View();



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
