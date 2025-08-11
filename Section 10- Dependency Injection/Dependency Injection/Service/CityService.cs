using ServiceContract;

namespace Services
{
	public class CityService:ICityService
	{
		private List<string> Cities;

		public CityService()
		{
			Cities = new List<string>()
			{
				"aa",
				"bb",
				"cc",
				"dd",
				"ee",
				"ff"
			};
		}

		public List<string> GetCountries()
		{
			return Cities;
		}
	}
}
