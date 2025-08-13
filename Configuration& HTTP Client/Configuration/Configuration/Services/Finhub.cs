using Configuration.ServiceContract;
using System.Text.Json;
namespace Configuration.Services
{
	public class Finhub:IFinhub
	{
		private readonly IHttpClientFactory _httpclientfactory;

		private readonly IConfiguration _configuration;

		public Finhub(IHttpClientFactory httpclientfactory, IConfiguration configuration)
		{
			_httpclientfactory = httpclientfactory;
			_configuration = configuration;
		}


		public async Task<Dictionary<string,object>> GetData(string symbol)
		{
			//why we made the using? --> because when the using ends the httpclientfactory ends connection with server
			using (HttpClient httpclient=_httpclientfactory.CreateClient())
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
				{ RequestUri= new Uri($"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_configuration["Finhub Token"]}"),
				Method=HttpMethod.Get
				};

				  HttpResponseMessage httpResponse = await httpclient.SendAsync(httpRequestMessage);

				Stream stream = httpResponse.Content.ReadAsStream();

				StreamReader streamReader = new StreamReader(stream);

				string response= streamReader.ReadToEnd();

				Dictionary<string,object> responsedict= JsonSerializer.Deserialize<Dictionary<string, object>>(response);

				return responsedict;
			}
		}
	}
}
