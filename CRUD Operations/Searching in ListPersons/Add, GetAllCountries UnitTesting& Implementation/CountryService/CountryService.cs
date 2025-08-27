using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Service
{
	public class CountryService : ICountryService
	{
		private List<Country> _countries;

		public CountryService()
		{
			_countries = new List<Country>();
		}

		CountryResponse ICountryService.AddCountry(CountryAddRequest? countryAddRequestobj)
		{
			//create validations for the unit test to check that everythingis alright
			//1-validation ofr obj is null
			if (countryAddRequestobj==null)
			{
				throw new ArgumentNullException(nameof(countryAddRequestobj));
			}
			//2-validatoion of country name is null
			if (countryAddRequestobj.Countryname==null)
			{
				throw new ArgumentException(nameof(countryAddRequestobj.Countryname));
			}
			//3-validation for duplicate country namne
			if (_countries.Where(country=>country.Countryname==countryAddRequestobj.Countryname).Count() >0)
			{
				throw new ArgumentException("nameof country isalready exists");
			}
			//convert CountryAddRequest obj into Country obj
			Country country=countryAddRequestobj.ToCountry();

			//give the obj an id
			country.CountryID = Guid.NewGuid();

			//addingthe country to the list

			_countries.Add(country);
			//converting country into countryresponse to return it
			CountryResponse countryResponse = country.ToCountryResponse();

			return countryResponse;

		}

		public List<CountryResponse> GetAllCountries() //retrieve List of countryresponse that contains  all the countries
		{
			List<CountryResponse> countryResponses = new List<CountryResponse>();

			foreach (Country country in _countries)
			{
				countryResponses.Add(country.ToCountryResponse());
			}

			return countryResponses;

			//return _countries.Select(country => country.ToCountryResponse()).ToList(); -->same code as above
			//_countries.Select(country => country.ToCountryResponse()).ToList()-->returns list of CountryResponse

		}
	}
}
