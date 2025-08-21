namespace Entities
{
	/// <summary>
	/// Domain model for Country (DB Model)
	/// </summary>
	public class Country
	{
		//put here the properties that will be stored for the country modelin the DB

		public Guid CountryID { get; set; }

		public string? Countryname { get; set; }

	}
}
