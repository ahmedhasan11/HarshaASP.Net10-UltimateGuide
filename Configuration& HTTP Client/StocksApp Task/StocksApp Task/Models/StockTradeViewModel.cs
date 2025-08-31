namespace StocksApp_Task.Models
{
	//This class will be used to send model object from controller to the "Trade/Index" view
	public class StockTradeViewModel
	{
		public string? StockSymbol { get; set; }

		public string? StockName { get; set; }

		public double Price { get; set; }

		public uint Quantity { get; set; }
	}
}
