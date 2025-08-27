using Entities;
using Service.Helpers;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service
{
	public class PersonService : IPersonService
	{
		private readonly List<Person> _persons;
		private readonly ICountryService _countryservice;

		public PersonService()
		{
			_persons = new List<Person>();
			_countryservice = new CountryService();
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
				case nameof(Person.PersonName):
					//MatchingList.Where(p => p.PersonName.Contains(SearchString));	
					////you have to check if the personname is null	
					MatchingList = ActualList.Where(p => (!string.IsNullOrEmpty(p.PersonName) ?
					p.PersonName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();								
					break;
				case nameof(Person.EmailAddress):
					MatchingList = ActualList.Where(p => (!string.IsNullOrEmpty(p.EmailAddress)
					? p.EmailAddress.Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();			
					break;
				case nameof(Person.Gender):
					MatchingList = ActualList.Where(p => (!string.IsNullOrEmpty(p.Gender)
					? p.Gender.Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
					break;
				case nameof(Person.CountryID):
					MatchingList = ActualList.Where(p => (p.Country != null) ? p.Country.ToString().Contains(SearchString) : true).ToList();
					break;
				case nameof(Person.Address):
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

		//public List<PersonResponse> GetAllPersons()
		//{
		//	throw new NotImplementedException();
		//}
	}
}
