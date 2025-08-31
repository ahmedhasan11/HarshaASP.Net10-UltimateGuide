namespace StocksApp_Task.ServiceContracts
{
	public interface IFinHubService
	{
		 Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);

		Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
	}
}
