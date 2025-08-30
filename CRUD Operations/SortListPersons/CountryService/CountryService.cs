using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Service
{
	public class CountryService : ICountryService
	{
		private List<Country> _countries;

		public CountryService(bool MockCountry=true)
		{
			_countries = new List<Country>();
			if (MockCountry)
			{
				_countries.AddRange(new Country() { CountryID = Guid.Parse("6D10A7D4-980B-4E4B-B8FE-8AA8E2BFDD29"), Countryname = "USA" },
				new Country() { CountryID = Guid.Parse("5436E255-CDAC-4814-9C37-C11452ED064C"), Countryname = "KSA" },
				new Country() { CountryID = Guid.Parse("460A5CB2-98FA-4718-9D63-6B2E29CE1033"), Countryname = "US" },
				new Country() { CountryID = Guid.Parse("755407A1-5E7E-4C4E-AA14-BC464D5608A6"), Countryname = "EGY" },
				new Country() { CountryID = Guid.Parse("400B8DC9-CE08-4FA3-9236-D02571F04B24"), Countryname = "Tanzania" });
			}
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

		public CountryResponse? GetCountryByID(Guid? countryID)
		{
			if (countryID==null)
			{
				return null;
			}

			//getting the country
			Country? country = _countries.Where(country => country.CountryID == countryID).FirstOrDefault();
			if (country == null)
			{
				return null;

			}
			CountryResponse response = country.ToCountryResponse();
			return response;
		}
	}
}
