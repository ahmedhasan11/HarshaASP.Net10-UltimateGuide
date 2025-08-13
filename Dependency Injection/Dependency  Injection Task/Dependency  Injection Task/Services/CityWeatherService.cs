using Dependency__Injection_Task.Models;
using Dependency__Injection_Task.ServiceContract;
using System.Diagnostics.Eventing.Reader;

namespace Dependency__Injection_Task.Services
{
	public class CityWeatherService:ICityWeather
	{

		private List<CityWeatherClass> Cities;
		public CityWeatherService()
		{
			Cities = new List<CityWeatherClass>() {
			new CityWeatherClass(){ CityUniqueCode = "LDN", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 8:00"),  Temp = 33},
			new CityWeatherClass(){CityUniqueCode = "NYC", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 3:00"),  Temp = 60},
			new CityWeatherClass(){CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Parse("2030-01-01 9:00"),  Temp = 82}
			};
		}

		public List<CityWeatherClass> GetCities()
		{
			return Cities;		
		}

		public CityWeatherClass GetCityByID(string citycode)
		{
			foreach (var city in Cities)
			{
				if (city.CityUniqueCode==citycode)
				{
					return city;
				}
			}
			return null;
		}
	}
}
