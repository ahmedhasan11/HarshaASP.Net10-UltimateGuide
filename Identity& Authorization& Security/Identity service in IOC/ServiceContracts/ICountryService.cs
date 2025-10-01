 using ServiceContracts.DTO;

namespace ServiceContracts
{
	/// <summary>
	/// business logic for manipulatingcountry entity
	/// </summary>
	public interface ICountryService
	{
		/// <summary>
		/// Adds country objto thelist of countries
		/// </summary>
		/// <param name="countryAddRequestobj"></param>
		/// <returns> returns the country obj after adding it</returns>
		Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequestobj);

		Task<List<CountryResponse>> GetAllCountries();

		Task<CountryResponse?> GetCountryByID(Guid? countryID);
	}
}
