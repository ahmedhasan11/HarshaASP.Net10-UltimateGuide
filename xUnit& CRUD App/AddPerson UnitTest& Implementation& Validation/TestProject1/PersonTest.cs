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

		public PersonTest()
		{
			_personservice = new PersonService();

		}
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
	}
}
