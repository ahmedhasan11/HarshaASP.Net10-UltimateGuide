using Microsoft.AspNetCore.Mvc;

namespace File_Result.Controller
{
    [Controller]
    public class HomeController:Microsoft.AspNetCore.Mvc.Controller
    {
        public ContentResult Index()
        {
			//can represent any type of content:Json,xml,html,text,pdf
			//  Content="Hello from Index" , ContentType="text/plain"
			//instead of the above code

			//return Content("string text","content-type");

			//return Content("Hello from Index","text/plain");
			return Content("<h1>hello from text</h1>", "text/html");
		}
    }
}
