using Microsoft.AspNetCore.Mvc;
using JSON_Result.Models;

namespace File_Result.Controller
{
    public class HomeController :Microsoft.AspNetCore.Mvc.Controller
    {
        //Json always takes object as a parameter
        public JsonResult Index()
        {
            Person prsn = new Person() { ID=1, FirstName="ahmed"};
            //return new JsonResult(prsn);

            //instead of above code

            return Json(prsn);
        }
    }
}
