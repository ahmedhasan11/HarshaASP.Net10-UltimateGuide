using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Application.Areas.Admin.Controllers
{
    [Area("Admin")] //to specify that this area is related to Admin
    [Authorize(Roles ="Admin")] //only adminis allowedto accessthese action methods of this controller 
    //can be ona specific actionmethod
    //can also be on the whole controller
    //so if a normal user try to request this actionmethod using query string he get accessdenied
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
