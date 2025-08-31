using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp_Task.Models;
using StocksApp_Task.ServiceContracts;
using System.Diagnostics;

namespace StocksApp_Task.Controllers
{
	[Route("[controller]")]
	public class TradeController : Controller
    {
        private readonly IFinHubService _FinHubService;
        private readonly IConfiguration _configuration;
        private readonly TradingOptionsoptionpattern _Tradingoptionpattern;

        public TradeController(IFinHubService FinHubService, IConfiguration configuration, IOptions<TradingOptionsoptionpattern> Tradingoptionpattern)
        {
            _FinHubService = FinHubService;
            _configuration = configuration;
            _Tradingoptionpattern = Tradingoptionpattern.Value;
        }
		[Route("/")]
		[Route("[action]")]
		[Route("~/[controller]")]
		public async Task<IActionResult> Index()
        {
            Dictionary<string,object> CompanyProfile= await _FinHubService.GetCompanyProfile(_Tradingoptionpattern.DefaultStockSymbol);
			Dictionary<string, object> StockPrices = await _FinHubService.GetStockPriceQuote(_Tradingoptionpattern.DefaultStockSymbol);

            StockTradeViewModel StockTrade = new StockTradeViewModel()
            {
                StockSymbol = _Tradingoptionpattern.DefaultStockSymbol,
            };

			if (CompanyProfile != null && StockPrices != null)
			{
				StockTrade = new StockTradeViewModel() { StockSymbol = Convert.ToString(CompanyProfile["ticker"]), StockName = Convert.ToString(CompanyProfile["name"]), Price = Convert.ToDouble(StockPrices["c"].ToString()) };
			}
            ViewBag.FinHubToken = _configuration["FinHub Token"];
			return View(StockTrade);
        }
    }
}
