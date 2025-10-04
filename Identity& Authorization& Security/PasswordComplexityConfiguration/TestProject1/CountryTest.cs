using Service;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Data;
using Xunit.Sdk;
using Entities;
using Microsoft.EntityFrameworkCore;
namespace CountryTest
{

	public class CountryTest
	{
		private readonly ICountryService _countryService;

		public CountryTest()
		{
			_countryService = new CountryService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options));
		}

		#region Add Country
		//check for all validations that you want to do , and for every onecreate a test method 

		//Test for CountryAddRequest obj is Null-->NullException



		//return type of Task is the same as void
		[Fact]
		public async Task CountryAdd_NullException()
		{
			//Arrange
			CountryAddRequest countryAddRequest = null;
			//Act

			//Assert --> Assert.throws<Expected Exception>
			await Assert.ThrowsAsync<ArgumentNullException>(async() =>
				{
					await _countryService.AddCountry(countryAddRequest);
				}
			);
			//so if the actual exception == expected exception -->test case passes

		}
		//check if country name is null-->ArgumentNullException
		[Fact]
		public async Task CountryAdd_CountryNameNull()
		{
			//Arrange
			CountryAddRequest countryAddRequest = new CountryAddRequest() { Countryname = null };


			//assert-->ArgumentException becausesome value is wrong (not all)
			await Assert.ThrowsAsync<ArgumentException>(async() =>
			{
				//act
				await _countryService.AddCountry(countryAddRequest);
			});

		}
		//Test for CountryName is Duplicate
		[Fact]
		public async Task CountryAdd_DuplicateName()
		{
			//arrange
			CountryAddRequest req1 = new CountryAddRequest() { Countryname = "USA" };
			CountryAddRequest req2 = new CountryAddRequest() { Countryname = "USA" };
			//assert
			await Assert.ThrowsAsync<ArgumentException>(async() =>
			{
				//act
				await _countryService.AddCountry(req1);
				await _countryService.AddCountry(req2);
			});
		}

		//if you supplied the alldetails , it haveto add it
		[Fact]
		public async Task CountryAdd_Adding()
		{
			//arrange
			CountryAddRequest countryAddRequest = new CountryAddRequest() { Countryname = "US" };
			//act
			CountryResponse countryResponse = await _countryService.AddCountry(countryAddRequest);
			List<CountryResponse> GetAll =await _countryService.GetAllCountries();
			//assert
			Assert.True(countryResponse.CountryID != Guid.Empty);
			Assert.Contains(countryResponse, GetAll);//make sure the added obj exists in the GetAllCountries();
		}
		#endregion

		#region Display All Countries
		//List of countries  have to be empty before adding any country
		[Fact]
		public async Task GetAll_Null()
		{
			//Arrange-->we dontneedit we will not do any data
			//Act
			List<CountryResponse> actualCountriesList = await _countryService.GetAllCountries();
			//Assert
			Assert.Empty(actualCountriesList); //if its empty -->passes the test

		}
		//if you added 3 countries,we wantto return these 3-->we have to make sure that the 3 added is in the actual getcountriesmethod
		[Fact]
		public async Task GetAll_addingCountries()
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
				AddedCountries.Add(await _countryService.AddCountry(country)); //added countries -->Expectedlist
			}

			List<CountryResponse> GetAll = await _countryService.GetAllCountries();//actual List

			foreach (CountryResponse expected in AddedCountries)
			{
				Assert.Contains(expected, GetAll);//if the GetAll Contains the expected-->test passed
			}

			//Assert
			//Assert.Contains(AddedCountries, GetAll);//youcant saythat a collection is in another collection

		}
		//check if list is null

		#endregion

		#region GetCountryByID

		//id is null
		[Fact]
		public async Task GetByID_NullCheck()
		{
			Guid? guid = null;

			CountryResponse? CountryNull = await _countryService.GetCountryByID(guid);

			Assert.Null(CountryNull); //if its null-->returns null
			//Assert.Throws<ArgumentNullException>(() =>
			//{
			//	_countryService.GetCountryByID(guid);
			//});
		}

		//if you suplly the validcountry ID it should return the valid counttryobj

		[Fact]
		public async Task GetByID_getting()
		{
			//Arrange
			CountryAddRequest countryAddRequest = new CountryAddRequest() { Countryname = "KSA" };
			CountryResponse countryresponse=await _countryService.AddCountry(countryAddRequest);
			//List<CountryResponse> CountriesList=_countryService.GetAllCountries();

			//Act
			CountryResponse? CountrybyID=await _countryService.GetCountryByID(countryresponse.CountryID);

			//Assert
			Assert.Equal(countryresponse,CountrybyID ); //it alsointernally calls the Equals we overridded			

		}
		#endregion
	}
}
