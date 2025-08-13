using Configuration.Models;
using Configuration.ServiceContract;
using Configuration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Configuration.Controllers
{
    public class HomeController : Controller
    {
		#region IConfiguration
		//DI on the IConfiguration Interface 
		// private readonly IConfiguration _configuration;

		//inject the configuration object in the controller

		//public HomeController(IConfiguration configuration)
		//{
		//    _configuration = configuration;
		//}
		#endregion
		#region  injecting options pattern
		//6-instead of using options pattern like this withall these code
		//we better need to inject it

		//private readonly OptionsPattern _options;
		//public HomeController(IOptions<OptionsPattern> options)
		//{
		//    _options = options.Value; //Value contains the object of the OptionsPattern class
		//}
		#endregion

		//private readonly Finhub _service;
		//public HomeController(Finhub service)
		//{
		//	_service = service;
		//}

		private readonly  Finhub _finhub;
		private readonly symbolOptionsPattern _objpattern;
		public HomeController(Finhub finhub, IOptions<symbolOptionsPattern> objpattern)
		{
			_finhub = finhub;
			_objpattern = objpattern.Value;
		}

		public async Task< IActionResult> Index()
        {
			////how to access the appsettings.json properties

			////1-_configuration["API_KEY"]
			//ViewBag.APIKEY = _configuration["API_KEY"];

			////2-_configuration.GetValue<string>("API_KEY")
			//ViewBag.API = _configuration.GetValue<string>("API_KEY");

			//         //3-if its a hierarichal configuration --> the key have some inital keys that have values 
			//         //you access it likethis -->_configuration["Hirarichal:keyname"]

			//         ViewBag.Hirarichal = _configuration["Hirarichal:first"];

			////4-use the Options Pattern (Check notes) --> Get<>
			//// (*)1--> GetSection("section name") .Get<ModelClassName>() --> this creates a new object of the model class , and check
			////for the properties names of the model class , if they are matched -->it assign the values to the properties of the obj

			//_configuration.GetSection("weatherAPI").Get<OptionsPattern>();

			////5-use the Options Pattern -->Bind
			////(*)2-->Create a new object of the modelclass , then use the GetSection("section name").Bind(obj_name)
			//OptionsPattern obj = new OptionsPattern();
			//         _configuration.GetSection("weatherAPI").Bind(obj);

			//         //6-instead of using options pattern like this withall these code
			//we better need to inject the options pattern in the constrcutor

			// ViewBag.ClientAPI= _options.ClientAPI;


			//making the request to finhub
			//await _service.GetData();


			Dictionary<string,object> DictResponse=await _finhub.GetData(_objpattern.DefaultSymbol);

			DictViewModel viewobj = new DictViewModel()
			{
				StockSymbol = _objpattern.DefaultSymbol,
				CurrentPrice = Convert.ToDouble(DictResponse["c"]),
				HighestPrice = Convert.ToDouble(DictResponse["h"]),
				LowestPrice = Convert.ToDouble(DictResponse["l"]),
				OpenPrice = Convert.ToDouble(DictResponse["o"]),

			};



			//then access these properties in the view to show them

            return View(viewobj);
        }
    }
}
