using CitiesManager.Web.DTO;
using CitiesManager.Web.Identity;
using CitiesManager.Web.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.Web.Controllers.v1
{
	[AllowAnonymous]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[ApiVersion("1.0")]

	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<ApplicationRole> _roleManager;
		private readonly IJwtService _jwtService;
		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IJwtService jwtService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_jwtService = jwtService;
		}

		[HttpPost]
		public async Task<ActionResult<ApplicationUser>> PostRegister(RegisterDTO registerDTO)
		{
			if (ModelState.IsValid == false)
			{
				string errormessages = string.Join(" | ", ModelState.Values.SelectMany(error => error.Errors).Select(err => err.ErrorMessage));

				return Problem(errormessages);
			}
			ApplicationUser user = new ApplicationUser()
			{
				PersonName = registerDTO.PersonName,
				Email = registerDTO.Email,
				UserName = registerDTO.Email,
				PhoneNumber = registerDTO.PhoneNumber
			};

			IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, isPersistent: false);
				AuthenticationResponse tokenresponse = _jwtService.CreateJwtToken(user);
				return Ok(tokenresponse);
			}
			else
			{
				//here you can collect the error messages that have been done from the Identity Result
				string errors = string.Join(" | ", result.Errors.Select(e => e.Description));//each elemnt have description property
																							 //thatcontains the actual error message
				return Problem(errors);
			}
		}

		[HttpPost("login")] //api/v1/Account/login
		public async Task<IActionResult> PostLogin(LoginDTO loginDTO)
		{
			if (ModelState.IsValid == false)
			{
				string errormessages = string.Join(" | ", ModelState.Values.SelectMany(errors => errors.Errors).Select(err => err.ErrorMessage));
				return Problem(errormessages);
			}
			//ApplicationUser? user=await _userManager.FindByEmailAsync(loginDTO.Email);

			var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: true, lockoutOnFailure: false);
			if (result.Succeeded)
			{
				ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);
				if (user == null)
				{
					return NoContent();
				}
				AuthenticationResponse tokenresponse= _jwtService.CreateJwtToken(user);
				return Ok(tokenresponse);
			}
			else
			{
				return Problem("Invalid Email or Password");
			}

		}


		[HttpGet("logout")] //api/v1/Account/logout
		public async Task<IActionResult> GetLogout()
		{
			await _signInManager.SignOutAsync();
			return NoContent();
		}

		[HttpGet]
		public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
		{
			ApplicationUser? user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return Ok(true);
			}
			else
			{
				return Ok(false);
			}
		}

	}
}
