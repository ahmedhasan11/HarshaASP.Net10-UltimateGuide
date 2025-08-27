using Entities;
using Service;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CountryTest
{

	public class PersonTest
	{
		private readonly IPersonService _personservice;
		private readonly ICountryService _countryservice;
		private readonly ITestOutputHelper _testoutputhelper;

		public PersonTest(ITestOutputHelper testOutputHelper)
		{
			_personservice = new PersonService();
			_countryservice = new CountryService();
			_testoutputhelper = testOutputHelper;//injecting testoutput helper service

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
			_testoutputhelper.WriteLine("expected:");
			_testoutputhelper.WriteLine( personAdded.ToString());
			List<PersonResponse> personsactuallist = _personservice.GetAllPersons();
			_testoutputhelper.WriteLine("actual list:");
			foreach (PersonResponse response in personsactuallist)
			{
				_testoutputhelper.WriteLine( response.ToString());
			}

			//Assert
			Assert.Contains(personAdded, personsactuallist);
		}
		#endregion

		#region GetFilteredPersons
		[Fact]
		//if the search string is empty & searchby is personname -->return all persons
		public void GetFilteredPersons_returnallpersons()
		{
			CountryAddRequest countryrequest1 = new CountryAddRequest() { Countryname = "tanzania" };
			CountryAddRequest countryrequest2 = new CountryAddRequest() { Countryname = "egypt" };
			CountryResponse country1 = _countryservice.AddCountry(countryrequest1);
			CountryResponse country2 = _countryservice.AddCountry(countryrequest2);

			PersonAddRequest personAddRequest1 = new PersonAddRequest()
			{
				PersonName = "ahmed",
				EmailAddress = "person1@example.com"
			,
				CountryID = country1.CountryID,
				ReccivenewsLetters = true,
				Address = "naklha street",
				Gender = ServiceContracts.Enums.GenderOptions.male
			,
				DateOfBirth = DateTime.Parse("2001-01-01")
			};
			PersonAddRequest personAddRequest2 = new PersonAddRequest()
			{
				PersonName = "ayman",
				EmailAddress = "person2@example.com"
			,
				CountryID = country2.CountryID,
				ReccivenewsLetters = true,
				Address = "ter3a street",
				Gender = ServiceContracts.Enums.GenderOptions.male
			,
				DateOfBirth = DateTime.Parse("2001-02-02")
			};
			List<PersonResponse> personresponses_add = new List<PersonResponse>(); //list of your data
			List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>() { personAddRequest1, personAddRequest2};
			foreach (PersonAddRequest personreq in personAddRequests)
			{
				PersonResponse resp = _personservice.AddPerson(personreq);
				personresponses_add.Add(resp);
			}


			//nameof()-->the name ofproperty whoic you would like to search
			List<PersonResponse> filteredlist = _personservice.GetFilteredPersons(nameof(Person.PersonName), "");
			//this should returns all existingpersobns whateveer they are

			_testoutputhelper.WriteLine("ActualList:");
			foreach(PersonResponse fromfiletered in filteredlist)
			{
				_testoutputhelper.WriteLine(filteredlist.ToString());
			}



			_testoutputhelper.WriteLine("Expected:");
			foreach (PersonResponse expected in personresponses_add)
			{
				_testoutputhelper.WriteLine(expected.ToString());
				Assert.Contains(expected, filteredlist); //eah person in fltered should exist in the actual list
			}



		}
		//we will add few persons , and try to search using search strings ,
		[Fact]
		public void GetFilteredPersons_filtering()
		{
			CountryAddRequest countryrequest1 = new CountryAddRequest() { Countryname = "tanzania" };
		CountryAddRequest countryrequest2 = new CountryAddRequest() { Countryname = "egypt" };
		CountryResponse country1 = _countryservice.AddCountry(countryrequest1);
		CountryResponse country2 = _countryservice.AddCountry(countryrequest2);

		PersonAddRequest personAddRequest1 = new PersonAddRequest()
		{
			PersonName = "ahmed",
			EmailAddress = "person1@example.com"
		,
			CountryID = country1.CountryID,
			ReccivenewsLetters = true,
			Address = "naklha street",
			Gender = ServiceContracts.Enums.GenderOptions.male
		,
			DateOfBirth = DateTime.Parse("2001-01-01")
		};
		PersonAddRequest personAddRequest2 = new PersonAddRequest()
		{
			PersonName = "ayman",
			EmailAddress = "person2@example.com"
		,
			CountryID = country2.CountryID,
			ReccivenewsLetters = true,
			Address = "ter3a street",
			Gender = ServiceContracts.Enums.GenderOptions.male
		,
			DateOfBirth = DateTime.Parse("2001-02-02")
		};
		List<PersonResponse> personresponses_add = new List<PersonResponse>(); //list of your data
		List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>() { personAddRequest1, personAddRequest2 };
			foreach (PersonAddRequest personreq in personAddRequests)
			{
				PersonResponse resp = _personservice.AddPerson(personreq);
		personresponses_add.Add(resp);
			}


			//nameof()-->the name ofproperty whoic you would like to search
			List<PersonResponse> filteredlist = _personservice.GetFilteredPersons(nameof(Person.PersonName), "a");
			//it should work as case insensitive
			//this should returns all existingpersobns whateveer they are

			_testoutputhelper.WriteLine("ActualList:");
			foreach(PersonResponse fromfiletered in filteredlist)
			{
				_testoutputhelper.WriteLine(filteredlist.ToString());
			}



			_testoutputhelper.WriteLine("Expected:");
			foreach (PersonResponse expected in personresponses_add)
			{
				_testoutputhelper.WriteLine(expected.ToString());
			}

			foreach (PersonResponse PersonAddresponse in personresponses_add)
			{
				if (PersonAddresponse.PersonName!=null)
				{
					if (PersonAddresponse.PersonName.Contains("a", StringComparison.OrdinalIgnoreCase))
					{
						Assert.Contains(PersonAddresponse, filteredlist);
					}
				}


			}


		}
		#endregion
	}
}
