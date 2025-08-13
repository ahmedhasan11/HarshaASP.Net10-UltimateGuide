using Dependency__Injection_Task.Models;

namespace Dependency__Injection_Task.ServiceContract
{
	public interface ICityWeather
	{
		List<CityWeatherClass> GetCities();

		CityWeatherClass GetCityByID(string citycode);

	}
}
