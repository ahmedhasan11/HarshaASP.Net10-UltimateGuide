using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Service
{
	public class CountryService : ICountryService
	{
		private PersonsDbContext _db;

		public CountryService(PersonsDbContext db)
		{
			_db = db;

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
			if (await _db.Countries.CountAsync(temp => temp.Countryname == countryAddRequestobj.Countryname) > 0)
			{
				throw new ArgumentException("nameof country isalready exists");
			}
			//convert CountryAddRequest obj into Country obj
			Country country=countryAddRequestobj.ToCountry();

			//give the obj an id
			country.CountryID = Guid.NewGuid();

			//addingthe country to the list

			_db.Countries.Add(country);
			await _db.SaveChangesAsync();
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


			return await _db.Countries.Select(country => country.ToCountryResponse()).ToListAsync();

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
			Country? country = await _db.Countries.Where(country => country.CountryID == countryID).FirstOrDefaultAsync();
			if (country == null)
			{
				return null;

			}
			CountryResponse response = country.ToCountryResponse();
			return response;
		}
	}
}
