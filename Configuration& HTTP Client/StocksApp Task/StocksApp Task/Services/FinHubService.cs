using StocksApp_Task.ServiceContracts;
using System.Text.Json;

namespace StocksApp_Task.Services
{
	public class FinHubService : IFinHubService
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpClientFactory _httpClientFactory;
		public FinHubService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
		{
			_configuration = configuration;
			_httpClientFactory = httpClientFactory;
		}


		
		public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
		{
			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
				{
					RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinHub Token"]}"),
					Method=HttpMethod.Get

				};

				HttpResponseMessage httpresponse =await httpClient.SendAsync(httpRequestMessage);

				Stream stream = httpresponse.Content.ReadAsStream();

				StreamReader streamReader = new StreamReader(stream);

				string response = streamReader.ReadToEnd();

				Dictionary<string, object>? responseDict = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

				return responseDict;

			}
		}
		public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
		{
			using (HttpClient httpclient = _httpClientFactory.CreateClient())
			{
				HttpRequestMessage requestMessage = new HttpRequestMessage()
				{
					RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinHub Token"]}"),
					Method = HttpMethod.Get
				};

				HttpResponseMessage httpResponse = await httpclient.SendAsync(requestMessage);

				Stream stream = httpResponse.Content.ReadAsStream();

				StreamReader streamReader = new StreamReader(stream);

				string response = streamReader.ReadToEnd();

				Dictionary<string, object>? responseDict = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

				return responseDict;
			}

		}
	}
}
