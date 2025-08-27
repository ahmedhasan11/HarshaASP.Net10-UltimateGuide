using Service;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Data;
using Xunit.Sdk;
namespace CountryTest
{

	public class CountryTest
	{
		private readonly ICountryService _countryService;

		public CountryTest()
		{
			_countryService = new CountryService();
		}

		#region Add Country
		//check for all validations that you want to do , and for every onecreate a test method 

		//Test for CountryAddRequest obj is Null-->NullException
		[Fact]
		public void CountryAdd_NullException()
		{
			//Arrange
			CountryAddRequest countryAddRequest = null;
			//Act

			//Assert --> Assert.throws<Expected Exception>
			Assert.Throws<ArgumentNullException>(
				() =>
				{
					_countryService.AddCountry(countryAddRequest);
				}
			);
			//so if the actual exception == expected exception -->test case passed


			//Act



		}

		//check if country name is null-->ArgumentNullException

		[Fact]
		public void CountryAdd_CountryNameNull()
		{
			//Arrange
			CountryAddRequest countryAddRequest = new CountryAddRequest() { Countryname = null };


			//assert-->ArgumentException becausesome value is wrong (not all)
			Assert.Throws<ArgumentException>(() =>
			{
				//act
				_countryService.AddCountry(countryAddRequest);
			});

		}
		//Test for CountryName is Duplicate
		[Fact]
		public void CountryAdd_DuplicateName()
		{
			//arrange
			CountryAddRequest req1 = new CountryAddRequest() { Countryname = "USA" };
			CountryAddRequest req2 = new CountryAddRequest() { Countryname = "USA" };


			//assert
			Assert.Throws<ArgumentException>(() =>
			{
				//act
				_countryService.AddCountry(req1);
				_countryService.AddCountry(req2);
			});
		}

		//if you supplied the alldetails , it haveto add it
		[Fact]
		public void CountryAdd_Adding()
		{
			//arrange
			CountryAddRequest countryAddRequest = new CountryAddRequest() { Countryname = "US" };


			//act
			CountryResponse countryResponse = _countryService.AddCountry(countryAddRequest);
			List<CountryResponse> GetAll = _countryService.GetAllCountries();
			//assert
			Assert.True(countryResponse.CountryID != Guid.Empty);
			Assert.Contains(countryResponse, GetAll);//make sure the added obj exists in the GetAllCountries();






		}
		#endregion

		#region Display All Countries
		//List of countries  have to be empty before adding any country
		[Fact]
		public void GetAll_Null()
		{
			//Arrange-->we dontneedit we will not do any data
			//Act
			List<CountryResponse> actualCountriesList = _countryService.GetAllCountries();
			//Assert
			Assert.Empty(actualCountriesList); //if its empty -->passes the test

		}
		//if you added 3 countries,we wantto return these 3-->we have to make sure that the 3 added is in the actual getcountriesmethod
		[Fact]
		public void GetAll_addingCountries()
		{
			//Arrange-->

			//List of the countries we want to add
			List<CountryAddRequest> Newcountries = new List<CountryAddRequest>() { new CountryAddRequest() {Countryname="USA" },
			new CountryAddRequest(){Countryname="UK"},
			new CountryAddRequest(){Countryname="Saudi"}};


			//Act
			List<CountryResponse> AddedCountries = new List<CountryResponse>();//list of added Countries

			foreach (CountryAddRequest country in Newcountries)
			{
				//adding the country to the GetAll list 
				AddedCountries.Add(_countryService.AddCountry(country)); //added countries -->Expectedlist
			}

			List<CountryResponse> GetAll = _countryService.GetAllCountries();//actual List

			foreach (CountryResponse expected in AddedCountries)
			{
				Assert.Contains(expected, GetAll);//if the GetAll Contains the expected-->test passed
			}

			//Assert
			//Assert.Contains(AddedCountries, GetAll);//youcant saythat a collection is in another collection

		}
		//check if list is null

		#endregion
	}
}
