using Entities;
using Service;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryTest
{

	public class PersonTest
	{
		private readonly IPersonService _personservice;
		private readonly ICountryService _countryservice;

		public PersonTest()
		{
			_personservice = new PersonService();
			_countryservice = new CountryService();

		}

		#region Add Person

		//person obj null
		[Fact]
		public void AddPerson_PersonNull()
		{
			//Arrange
			PersonAddRequest? personAddRequest = null;
			//Act
			//	PersonResponse personresponse=_personservice.AddPerson(personAddRequest);
			//Assert
			Assert.Throws<ArgumentNullException>(() =>
			{
				_personservice.AddPerson(personAddRequest);
			});
		}
		//person obj name property is null
		[Fact]
		public void AddPerson_PersonNameNull()
		{
			PersonAddRequest personAddRequest = new PersonAddRequest() { PersonName = null };
			Assert.Throws<ArgumentException>(() =>
			{
				_personservice.AddPerson(personAddRequest);
			});

		}

		//whenyouadd a person ithave to beinserted into the personlist
		[Fact]
		public void AddPerson_insertperson()
		{
			//arrange
			PersonAddRequest personAddRequest = new PersonAddRequest()
			{
				Address = "sdadad",
				EmailAddress = "person@example.com",
				ReccivenewsLetters = true,
				PersonName = "ahmed",
				CountryID = Guid.NewGuid(),
				Gender = ServiceContracts.Enums.GenderOptions.male,
				DateOfBirth = DateTime.Parse("2001-01-01")
			};
			//act
			PersonResponse person = _personservice.AddPerson(personAddRequest);
			//List<PersonResponse> Persons = _personservice.GetAllPersons();

			//assert
			Assert.True(person.PersonID != Guid.Empty);
			//Assert.Contains(person, Persons);
		}
		#endregion

		#region GetPersonByID
		[Fact]
		public void GetPersonByID_Null()
		{
			//Arrange
			Guid? guid = null;
			//Act
			PersonResponse addedperson = _personservice.GetPersonByID(guid);
			//Assert
			Assert.Null(addedperson); //if added perosn is null-->test passed
		}
		[Fact]
		public void GetPersonByID_checkresponse()
		{
			//Arrange
			CountryAddRequest countryrequest = new CountryAddRequest() { Countryname = "mansoura" };

			CountryResponse country=_countryservice.AddCountry(countryrequest);

			PersonAddRequest personAddRequest = new PersonAddRequest() { PersonName = "ahmed", EmailAddress = "person@example.com" 
			,CountryID=country.CountryID, ReccivenewsLetters=true, Address="naklha street", Gender=ServiceContracts.Enums.GenderOptions.male
			,DateOfBirth=DateTime.Parse("2001-01-01")};
			//Act
			PersonResponse addedperson=_personservice.AddPerson(personAddRequest);
			PersonResponse getperson = _personservice.GetPersonByID(addedperson.PersonID);
			//Assert
			Assert.Equal(addedperson, getperson); //test passed if both objects are the same
			//Assert.True(getperson.PersonID != Guid.Empty);
			//Assert.Contains
		}
		#endregion

		#region GetAllPersons
		[Fact]
		//the GetAllPersons() should return empty list by default
		public void GetAllPersons_Null()
		{
			List<PersonResponse> personslist = _personservice.GetAllPersons();
			//Assert
			Assert.Empty(personslist);
		}
		[Fact]
		//try to add person then see if that person is in the GteAllPersons
		public void GetAllPersons_insertperson()
		{
			//Arrange
			CountryAddRequest countryrequest = new CountryAddRequest() { Countryname = "tanzania" };
			CountryResponse country=_countryservice.AddCountry(countryrequest);
			PersonAddRequest personAddRequest = new PersonAddRequest()
			{
				PersonName = "ahmed",
				EmailAddress = "person@example.com"
			,
				CountryID = country.CountryID,
				ReccivenewsLetters = true,
				Address = "naklha street",
				Gender = ServiceContracts.Enums.GenderOptions.male
			,
				DateOfBirth = DateTime.Parse("2001-01-01")
			};
			//Act
			PersonResponse personAdded = _personservice.AddPerson(personAddRequest);
			List<PersonResponse> personsactuallist = _personservice.GetAllPersons();
			//Assert
			Assert.Contains(personAdded, personsactuallist);
		}
		#endregion
	}
}
