using Microsoft.AspNetCore.Mvc;
using Controllers_Task.Models;

namespace Controllers_Task.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("/")]
		public IActionResult Welcome()
		{
            Account acc = new Account()
            {AccountNumber=1001 , AccountCurrentBalance=5000 , AccountHolderName="Example Name" };

			return Content("Welcome to the Best Bank","text/html");
		}
		[Route("/account-details")]
		public IActionResult Details()
		{
			Account acc = new Account()
			{ AccountNumber = 1001, AccountCurrentBalance = 5000, AccountHolderName = "Example Name" };

			return Json(acc);
		}
		[Route("/account-statement")]
		public IActionResult Dummy()
		{
			Account acc = new Account()
			{ AccountNumber = 1001, AccountCurrentBalance = 5000, AccountHolderName = "Example Name" };

			return PhysicalFile("C:\\Users\\user\\source\\repos\\.NET\\Harsha-ASP.NET-Complete-Guide\\Controller\\Controllers Task\\Controllers Task\\Ahmed-Hasan-ELbadawy-FlowCV-Resume-20250126 - Copy.pdf","application/pdf");
		}
		[Route("/get-current-balance/{accountNumber:int?}")]
			public IActionResult GetCurrentBalance()
			{
				// Get the 'accountNumber' value from the route parameters using RouteData
				object accountNumberObj;
				if (HttpContext.Request.RouteValues.TryGetValue("accountNumber", out accountNumberObj) && accountNumberObj is string accountNumber)
				{
					// Check if the 'accountNumber' parameter is provided
					if (string.IsNullOrEmpty(accountNumber))
					{
						return NotFound("Account Number should be supplied");
					}

					// Convert the 'accountNumber' to an integer
					if (int.TryParse(accountNumber, out int accountNumberInt))
					{
						// Hard-coded data
						var bankAccount = new { accountNumber = 1001, accountHolderName = "Example Name", currentBalance = 5000 };

						if (accountNumberInt == 1001)
						{
							// If accountNumber is 1001, return the current balance value
							return Content(bankAccount.currentBalance.ToString());
						}
						else
						{
							// If accountNumber is not 1001, return HTTP 400
							return BadRequest("Account Number should be 1001");
						}
					}
					else
					{
						// If the 'accountNumber' provided in the route parameter is not a valid integer, return HTTP 400
						return BadRequest("Invalid Account Number format");
					}
				}
				else
				{
					// If 'accountNumber' is not found in the route parameters, handle the error
					// For example, redirect to an error page or return a specific error message
					// return RedirectToAction("Error");
					return NotFound("Account Number should be supplied");
				}

			}
	}
}
