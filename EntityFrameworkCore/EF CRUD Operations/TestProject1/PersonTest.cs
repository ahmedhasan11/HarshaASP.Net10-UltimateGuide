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

		public PersonTest(ITestOutputHelper testOutputHelper, IPersonService personService, ICountryService countryService)
		{
			_personservice = personService;
			_countryservice = countryService;
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

		#region GetSortedPersons

		[Fact]
		//when we sort absed on personname descending it should returns list of person name in Desc
		public void GetSortedPersons_Desc()
		{
			//Arrange
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

			//Act
			foreach (PersonAddRequest personreq in personAddRequests)
			{
				PersonResponse resp = _personservice.AddPerson(personreq);
				personresponses_add.Add(resp);
			}

			List<PersonResponse> PersonresponsefromGet = _personservice.GetAllPersons();

			List<PersonResponse> SortedList = _personservice.GetSortedPersons(
				PersonresponsefromGet, nameof(Person.PersonName), ServiceContracts.Enums.SortOrderEnum.DESC);

			personresponses_add = personresponses_add.OrderByDescending(p => p.PersonName).ToList();

				//you have to check if the first obj in the AddList = first obj of SortedList
				//Second obj= second obj and so on

			//Assert
			for (int i=0; i<personresponses_add.Count; i++)
			{
			Assert.Equal(personresponses_add[i], SortedList[i]);
			}

		}
		#endregion

		#region UpdatePerson
		//when we supply PersonUpdateRequest as Null-->throw argumentnull exception
		[Fact]
		public void UpdatePerson_NullObj()
		{
			//Arrange
			PersonUpdateRequest? personUpdateRequest = null;

			//Assert
			Assert.Throws<ArgumentNullException>(() =>
			{
				//Act
				_personservice.UpdatePerson(personUpdateRequest);
			});

		}
		//when we supply InvalidID-->throw argumentexception
		[Fact]
		public void UpdatePerson_InvalidID()
		{
			//Arrange
			PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest() { PersonID = Guid.NewGuid() };

			//Assert
			Assert.Throws<ArgumentException>(() =>
			{
				//Act
				_personservice.UpdatePerson(personUpdateRequest);
			});

		}
		//when we supply PersonName is Null-->throw argumentexception
		[Fact]
		public void UpdatePerson_NameNull()
		{
			CountryAddRequest countryrequest1 = new CountryAddRequest() { Countryname = "tanzania" };
			CountryResponse country1 = _countryservice.AddCountry(countryrequest1);
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
			PersonResponse personResponse = _personservice.AddPerson(personAddRequest1);

			//Arrange
			PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
			//copies all valuesinto a peronupdaterequestobj

			personUpdateRequest.PersonName = null; //updating person name

			//Assert
			Assert.Throws<ArgumentException>(() =>
			{
				//Act
				_personservice.UpdatePerson(personUpdateRequest);
			});

		}

		//add a person and try to update his name & Email 
		[Fact]
		public void UpdatePerson_Updating()
		{
			CountryAddRequest countryrequest1 = new CountryAddRequest() { Countryname = "tanzania" };
			CountryResponse country1 = _countryservice.AddCountry(countryrequest1);
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
			PersonResponse personResponse = _personservice.AddPerson(personAddRequest1);

			//Arrange
			PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
			//copies all valuesinto a peronupdaterequestobj

			personUpdateRequest.PersonName = "mahmoud";
			personUpdateRequest.EmailAddress = "mahmoud@example.com";

			_testoutputhelper.WriteLine("Actual:");
			PersonResponse UpdatedPerson = _personservice.UpdatePerson(personUpdateRequest);
			_testoutputhelper.WriteLine(nameof(UpdatedPerson));

			_testoutputhelper.WriteLine("Expected:");
			PersonResponse PersonwithGetByID = _personservice.GetPersonByID(personResponse.PersonID);
			_testoutputhelper.WriteLine(nameof(PersonwithGetByID));
			


			Assert.Equal(PersonwithGetByID, UpdatedPerson);


		}

		#endregion

		#region Delete Person
		[Fact]
		//if you supply valid id -->returns true
		public void DeletePerson_ValidID()
		{
			CountryAddRequest countryrequest1 = new CountryAddRequest() { Countryname = "tanzania" };
			CountryResponse country1 = _countryservice.AddCountry(countryrequest1);
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
			PersonResponse personResponse = _personservice.AddPerson(personAddRequest1);


			bool isdeleted = _personservice.DeletePerson(personResponse.PersonID);

			Assert.True(isdeleted);
				

		}
		[Fact]
		//if you supply an invalid id -->returns false
		public void DeletePerson_InvalidID()
		{
			bool isdeleted = _personservice.DeletePerson(Guid.NewGuid());
			Assert.False(isdeleted);
		}
		#endregion
	}
}
