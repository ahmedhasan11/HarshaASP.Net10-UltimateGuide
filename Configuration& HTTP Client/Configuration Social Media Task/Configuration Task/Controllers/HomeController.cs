using Configuration_Task.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Configuration_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly SocialLinks _optionpattern;
        private readonly IWebHostEnvironment _environment;

        public HomeController(IOptions<SocialLinks> optionpattern,IWebHostEnvironment environment)
        {
            _optionpattern = optionpattern.Value;
            _environment = environment;
        }
		[Route("/")]
        public IActionResult Index()
        {
			//if (_environment.IsDevelopment())
			//{
			//	SocialLinks links2 = new SocialLinks()
			//	{
			//		Facebook = _optionpattern.Facebook,
			//		Youtube = _optionpattern.Youtube,
			//		Twitter = _optionpattern.Twitter
			//	};
			//	return View(links2);
			//}

			SocialLinks links = new SocialLinks()
			{
					Facebook = _optionpattern.Facebook,
					Youtube = _optionpattern.Youtube,
					Instagram = _optionpattern.Instagram,
					Twitter = _optionpattern.Twitter
			};
			return View(links);
			


        }
    }
}
