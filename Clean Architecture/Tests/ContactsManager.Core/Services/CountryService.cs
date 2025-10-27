using Entities;
using Microsoft.AspNetCore.Mvc;

using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
	public class CountryService : ICountryService
	{
		private ICountriesRepository _countriesRepository;
	
		public CountryService(ICountriesRepository countriesRepository)
		{
			_countriesRepository = countriesRepository;

		}

		public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequestobj)
		{
			//create validations for the unit test to check that everythingis alright
			//1-validation ofr obj is null
			if (countryAddRequestobj == null)
			{
				throw new ArgumentNullException(nameof(countryAddRequestobj));
			}
			//2-validatoion of country name is null
			if (countryAddRequestobj.Countryname == null)
			{
				throw new ArgumentException(nameof(countryAddRequestobj.Countryname));
			}
			//3-validation for duplicate country namne
			//await _db.Countries.CountAsync(temp => temp.Countryname == countryAddRequestobj.Countryname) > 0
			if (await _countriesRepository.GetCountryByCountryName(countryAddRequestobj.Countryname)!=null)
			{
				throw new ArgumentException("nameof country isalready exists");
			}
			//convert CountryAddRequest obj into Country obj
			Country country=countryAddRequestobj.ToCountry();

			//give the obj an id
			country.CountryID = Guid.NewGuid();

			//addingthe country to the list

			await _countriesRepository.AddCountry(country);
			//converting country into countryresponse to return it
			CountryResponse countryResponse = country.ToCountryResponse();

			return countryResponse;

		}

		public async Task<List<CountryResponse>> GetAllCountries() //retrieve List of countryresponse that contains  all the countries
		{
			//List<CountryResponse> countryResponses = new List<CountryResponse>();

			//foreach (Country country in  _db.Countries)
			//{
			//	countryResponses.Add(country.ToCountryResponse());
			//}

			//we made these bracketts because weneed this statment to be executed first 
			//so then we can make our second query which is select
			return (await _countriesRepository.GetAllCountries()).Select(country => country.ToCountryResponse()).ToList();

			//return countryResponses;

			//return _countries.Select(country => country.ToCountryResponse()).ToList(); -->same code as above
			//_countries.Select(country => country.ToCountryResponse()).ToList()-->returns list of CountryResponse

		}

		public async Task< CountryResponse?> GetCountryByID(Guid? countryID)
		{
			if (countryID==null)
			{
				return null;
			}

			//getting the country
			//because the countryID is nullable , wehave to use countryID.value to get its value
			Country? country = await _countriesRepository.GetCountryByCountryID(countryID.Value);
			if (country == null)
			{
				return null;

			}
			CountryResponse response = country.ToCountryResponse();
			return response;
		}
	}
}
