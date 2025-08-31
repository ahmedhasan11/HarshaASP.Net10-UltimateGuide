namespace StocksApp_Task.Models
{
	public class GetStockPricesViewModel
	{
		//public string? StockSymbol { get; set; }

		public double CurrentPrice { get; set; }
		public double Change { get; set; }
		public double PercentChange { get; set; }
		public double HighestPrice { get; set; }
		public double LowestPrice { get; set; }

		public double OpenPrice { get; set; }
		public double PreviousClosePrice { get; set; }
	}
}
