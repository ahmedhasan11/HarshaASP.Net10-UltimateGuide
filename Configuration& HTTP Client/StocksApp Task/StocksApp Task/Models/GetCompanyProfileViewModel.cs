namespace StocksApp_Task.Models
{
	public class GetCompanyProfileViewModel
	{
		public string? StockSymbol { get; set; }

		public string? country { get; set; }
		public string? currency { get; set; }
		public string? exchange { get; set; }
		public string? finhubindustryclassification { get; set; }
		public DateTime? ipo { get; set; }

		public string? logo { get; set; }
		public double? marketcapitalization { get; set; }
		public string? companyname { get; set; }
		public int? companyphonenumber { get; set; }
		public double? shareOutstanding { get; set; }
		public string? ticker { get; set; }
		public string? weburl { get; set; }

	}
}
