using Entities;
using Service.Helpers;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace Service
{
	public class PersonService : IPersonService
	{
		private readonly List<Person> _persons;
		private readonly ICountryService _countryservice;

		public PersonService(bool MockPerson=true)
		{
			_persons = new List<Person>();
			_countryservice = new CountryService();
			if (MockPerson)
			{
				_persons.AddRange(
				new Person() { PersonID = Guid.Parse("17B73155-8F63-43AD-90FE-53BB5E589812"), PersonName = "Farr", 
					Address = "276 Prairieview Parkway", EmailAddress = "fgready0@exblog.jp", DateOfBirth = DateTime.Parse("8/6/2025"),
					Gender = "Male", ReccivenewsLetters = false, CountryID = Guid.Parse("6D10A7D4-980B-4E4B-B8FE-8AA8E2BFDD29") },
				new Person() { PersonID = Guid.Parse("0271247B-BF76-4A64-96C0-68511B7C512B"), PersonName = "Griswold",
					Address = "85942 Sullivan Alley", EmailAddress = "gpaule1@ehow.com", DateOfBirth = DateTime.Parse("12/12/2024"),
					Gender = "Male", ReccivenewsLetters = true, CountryID = Guid.Parse("5436E255-CDAC-4814-9C37-C11452ED064C") },
				new Person() { PersonID = Guid.Parse("5F426564-8719-4179-9754-9AB7AD9F47CE"), PersonName = "Stephanus",
					Address = "88 Carberry Trail", EmailAddress = "sdavana2@bloglines.com", DateOfBirth = DateTime.Parse("9/2/2024"),
					Gender = "Male", ReccivenewsLetters = false, CountryID = Guid.Parse("460A5CB2-98FA-4718-9D63-6B2E29CE1033") },
				new Person() { PersonID = Guid.Parse("9B66050A-8D8C-4B70-81EB-50BB0E60BE14"), PersonName = "Pepillo",
					Address = "458 Schurz Lane", EmailAddress = "pthorington3@un.org", DateOfBirth = DateTime.Parse("10/4/2024"),
					Gender = "Male", ReccivenewsLetters = true, CountryID = Guid.Parse("755407A1-5E7E-4C4E-AA14-BC464D5608A6") },
				new Person() { PersonID = Guid.Parse("F946827D-2F5A-4EDF-A8A1-581C9C86EFBD"), PersonName = "Jereme",
					Address = "94 Lotheville Park", EmailAddress = "jwulfinger4@g.co", DateOfBirth = DateTime.Parse("6/10/2025"),
					Gender = "Female", ReccivenewsLetters = false, CountryID = Guid.Parse("6D10A7D4-980B-4E4B-B8FE-8AA8E2BFDD29") },
				new Person() { PersonID = Guid.Parse("B0B70BFB-8DC3-43D8-B815-B30CB1C68112"), PersonName = "Mara",
					Address = "9 Kedzie Junction", EmailAddress = "mgiacopello5@ebay.com", DateOfBirth = DateTime.Parse("1/26/2025"),
					Gender = "Female", ReccivenewsLetters = true, CountryID = Guid.Parse("400B8DC9-CE08-4FA3-9236-D02571F04B24") },
				new Person() { PersonID = Guid.Parse("D2A52A5E-ED34-4C4B-97DB-E3FE3602FBF3"), PersonName = "Mill",
					Address = "5004 Shasta Lane", EmailAddress = "mscamerdine6@tinypic.com", DateOfBirth = DateTime.Parse("4/22/2025"),
					Gender = "Female", ReccivenewsLetters = false, CountryID = Guid.Parse("460A5CB2-98FA-4718-9D63-6B2E29CE1033") },
				new Person() { PersonID = Guid.Parse("6F792249-3872-40A9-BB74-97ACF878D44E"), PersonName = "Yance",
					Address = "757 Browning Point", EmailAddress = "ycarlick7@addthis.com", DateOfBirth = DateTime.Parse("2/1/2025"), 
					Gender = "Male", ReccivenewsLetters = true, CountryID = Guid.Parse("400B8DC9-CE08-4FA3-9236-D02571F04B24") },
				new Person() { PersonID = Guid.Parse("7B5E6BAF-7207-40DF-898D-FCD204366A4A"), PersonName = "Ange",
					Address = "87842 Florence Way", EmailAddress = "aleftley8@psu.edu", DateOfBirth = DateTime.Parse("9/23/2024"),
					Gender = "Male", ReccivenewsLetters = false, CountryID = Guid.Parse("755407A1-5E7E-4C4E-AA14-BC464D5608A6") },
				new Person() { PersonID = Guid.Parse("70CC23A4-2858-4B90-8839-9298DB6B69B5"), PersonName = "Tann",
					Address = "09 Toban Place", EmailAddress = "tadamovitz9@intel.com", DateOfBirth = DateTime.Parse("5/27/2025"),
					Gender = "Male", ReccivenewsLetters = true, CountryID = Guid.Parse("755407A1-5E7E-4C4E-AA14-BC464D5608A6") }
			);



			}
		}
		public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
		{
			
			if (personAddRequest==null)
			{
				throw new ArgumentNullException(nameof(personAddRequest));
			}
			if (string.IsNullOrEmpty(personAddRequest.PersonName))
			{
				throw new ArgumentException(nameof(personAddRequest.PersonName));
			}
			//ValidationContext validationContext = new ValidationContext(personAddRequest);
			//List<ValidationResult> validationResults = new List<ValidationResult>();
			//Validator.TryValidateObject(personAddRequest, validationContext, validationResults, true);

			//instead of the code above
			PersonValidationHelper.ModelValidation(personAddRequest);
			Person person = personAddRequest.ToPerson();

			person.PersonID = Guid.NewGuid();

			_persons.Add(person);
			//person
			PersonResponse response = person.ToPersonResponse();
			//herewe want to get the country name from countryID ,so we candothis by usingthe GetCountryByID
			//function we created in CountryService
			//but you havetoinject it in your constructor to use it
			
			response.Country = _countryservice.GetCountryByID(person.CountryID)?.Countryname;
			//if you didnt put the "?" here it will give yopu null ref error , what the "?" do here
			//it says if that _countryservice.GetCountryByID(person.CountryID) returned null , dont access
			//this countryname property , and assign null to the Country peroperty 

			return response;
		}
		public PersonResponse GetPersonByID(Guid? personID)
		{
			if (personID==null)
			{
				return null;
			}
		
			Person? person= _persons.Where(p => p.PersonID == personID).FirstOrDefault();
			if (person==null)
			{
				throw new ArgumentNullException();
			}
			PersonResponse personResponse = person.ToPersonResponse();
			return personResponse;
		}
		public List<PersonResponse> GetAllPersons()
		{
			//List<PersonResponse> personResponses = new List<PersonResponse>();
			//foreach (Person person in _persons)
			//{
			//	PersonResponse personresponse=person.ToPersonResponse();
			//	personResponses.Add(personresponse);
			//}
			//if (personResponses==null)
			//{
			//	return null;
			//}
			//instead of doing the above code do that lambdaexperssion
			//this lambda expression gets each person and convert it to personresponse then returns it
			List<PersonResponse> personresponses = _persons.Select(person => person.ToPersonResponse()).ToList();
			if (personresponses==null)
			{
				return null;
			}
			return personresponses;
			//List<PersonResponse> personResponses = 
		}
		public List<PersonResponse> GetFilteredPersons(string SearchBy, string? SearchString)
		{
			//List<PersonResponse> ActualList = _persons.Select(p => p.ToPersonResponse()).ToList();
			List<PersonResponse> ActualList = GetAllPersons();
			List<PersonResponse> MatchingList = ActualList;//let it = actuallist because if there is no filters , it returns the whole list


			if (string.IsNullOrEmpty(SearchString))
			{
				return MatchingList;
			}

			//now i needto check if the searchby is really like a preperty in Person
			switch (SearchBy)
			{
				case nameof(PersonResponse.PersonName):
					//MatchingList.Where(p => p.PersonName.Contains(SearchString));	
					////you have to check if the personname is null	
					MatchingList = ActualList.Where(p => (!string.IsNullOrEmpty(p.PersonName) ?
					p.PersonName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();								
					break;
				case nameof(PersonResponse.EmailAddress):
					MatchingList = ActualList.Where(p => (!string.IsNullOrEmpty(p.EmailAddress)
					? p.EmailAddress.Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();			
					break;
				case nameof(PersonResponse.Gender):
					MatchingList = ActualList.Where(p => (!string.IsNullOrEmpty(p.Gender)
					? p.Gender.Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
					break;
				case nameof(PersonResponse.CountryID):
					MatchingList = ActualList.Where(p => (p.Country != null) ? p.Country.ToString().Contains(SearchString) : true).ToList();
					break;
				case nameof(PersonResponse.Address):
					MatchingList = ActualList.Where(p => (!string.IsNullOrEmpty(p.Address) ?
					p.Address.Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
					break;
			}

			return MatchingList;
		}

		public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string SortBy, SortOrderEnum SortOrder)
		{

			allPersons = GetAllPersons();
			if (string.IsNullOrEmpty(SortBy))
			{
				return allPersons;
			}

			/* Switch Normal
			switch (SortBy)
			{
				case nameof(Person.PersonName):
					if (SortOrder==SortOrderEnum.DESC)
					{
						SortedList = allPersons.OrderByDescending(p=> p.PersonName).ToList();
							//OrderByDescending(p => p.PersonName).ToList();
					}
					SortedList = allPersons.OrderBy(p => p.PersonName).ToList();
					break;

				case nameof(Person.Address):
					if (SortOrder == SortOrderEnum.DESC)
					{
						SortedList = allPersons.OrderByDescending(p => p.Address).ToList();
					}
					SortedList = allPersons.OrderBy(p => p.Address).ToList();
					break;

				case nameof(Person.EmailAddress):
					if (SortOrder == SortOrderEnum.DESC)
					{
						SortedList = allPersons.OrderByDescending(p => p.EmailAddress).ToList();
					}
					SortedList = allPersons.OrderBy(p => p.EmailAddress).ToList();
					break;

			}
			*/
			//Switch Expression:
			List<PersonResponse> SortedList = (SortBy, SortOrder)
			switch
			{
				//PersonName
				(nameof(PersonResponse.PersonName), SortOrderEnum.DESC) => allPersons.OrderByDescending(p => p.PersonName,
				StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.PersonName), SortOrderEnum.ASC) => allPersons.OrderBy(p => p.PersonName,
				StringComparer.OrdinalIgnoreCase).ToList(),

				//Addresss

				(nameof(PersonResponse.Address), SortOrderEnum.DESC) => allPersons.OrderByDescending(p => p.Address,
				StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.Address), SortOrderEnum.ASC) => allPersons.OrderBy(p => p.Address,
				StringComparer.OrdinalIgnoreCase).ToList(),

				//Email
				(nameof(PersonResponse.EmailAddress), SortOrderEnum.DESC) => allPersons.OrderByDescending(p => p.EmailAddress,
				StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.EmailAddress), SortOrderEnum.ASC) => allPersons.OrderBy(p => p.EmailAddress,
				StringComparer.OrdinalIgnoreCase).ToList(),

				//Gender
				(nameof(PersonResponse.Gender), SortOrderEnum.DESC) => allPersons.OrderByDescending(p => p.Gender,
				StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.Gender), SortOrderEnum.ASC) => allPersons.OrderBy(p => p.Gender,
				StringComparer.OrdinalIgnoreCase).ToList(),

				//Recievenewsletter
				(nameof(PersonResponse.ReccivenewsLetters), SortOrderEnum.DESC) => allPersons.OrderByDescending(p => p.ReccivenewsLetters
				).ToList(),

				(nameof(PersonResponse.ReccivenewsLetters), SortOrderEnum.ASC) => allPersons.OrderBy(p => p.ReccivenewsLetters
				).ToList(),

				//DOB
				(nameof(PersonResponse.DateOfBirth), SortOrderEnum.DESC) => allPersons.OrderByDescending(p => p.DateOfBirth
				).ToList(),

				(nameof(PersonResponse.DateOfBirth), SortOrderEnum.ASC) => allPersons.OrderBy(p => p.DateOfBirth.ToString()
				).ToList(),

				//CountryID
				(nameof(PersonResponse.Country), SortOrderEnum.DESC) => allPersons.OrderByDescending(p => p.Country,
				StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.Country), SortOrderEnum.ASC) => allPersons.OrderBy(p => p.Country,
				StringComparer.OrdinalIgnoreCase).ToList(),

				//_ means default
				_=>allPersons
			};
			return SortedList;
		}

		public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
		{
			List<PersonResponse> AllPersons = GetAllPersons();
			if (personUpdateRequest==null)
			{
				throw new ArgumentNullException("UpdateRequest is null");
			}

			PersonValidationHelper.ModelValidation(personUpdateRequest); //make validation on the obj
			if (personUpdateRequest.PersonName==null)
			{
				throw new ArgumentException("name cant be null");
			}

			//bool isexists = AllPersons.Any(p => p.PersonID == personUpdateRequest.PersonID);
			//PersonResponse FoundedPerson = GetPersonByID(personUpdateRequest.PersonID);//old data

			Person? FoundedPerson=_persons.FirstOrDefault(p => p.PersonID == personUpdateRequest.PersonID);
			if (FoundedPerson==null)
			{
				throw new ArgumentException("ID is invalid");
			}

			FoundedPerson.PersonName = personUpdateRequest.PersonName;
			FoundedPerson.Address = personUpdateRequest.Address;
			FoundedPerson.Gender = personUpdateRequest.Gender.ToString();
			FoundedPerson.ReccivenewsLetters = personUpdateRequest.ReccivenewsLetters;
			FoundedPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
			FoundedPerson.EmailAddress = personUpdateRequest.EmailAddress;
			FoundedPerson.CountryID = personUpdateRequest.CountryID;

			PersonResponse Response = FoundedPerson.ToPersonResponse();

			return Response;







		}

		public bool DeletePerson(Guid? PersonID)
		{
			if (PersonID==null)
			{
				return false;
			}
			Person? person = _persons.FirstOrDefault(p => p.PersonID == PersonID);
			if (person==null)
			{
				return false;
			}

			bool isdeleted=_persons.Remove(person);

			return isdeleted;


		}
	}
}
