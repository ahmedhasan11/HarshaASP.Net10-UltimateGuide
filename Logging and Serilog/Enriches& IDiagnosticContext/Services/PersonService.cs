using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RepositoryContracts;
using Microsoft.Extensions.Logging;
using Serilog;


namespace Services
{
	public class PersonService : IPersonService
	{
		private readonly IPersonsRepository _personsRepository;
		private readonly ILogger<PersonService> _logger;
		private readonly IDiagnosticContext _diagnosticContext;
		//private readonly ICountryService _countryservice;

		public PersonService(IPersonsRepository personsRepository, ILogger<PersonService> logger,IDiagnosticContext diagnosticContext /*ICountryService countryService*/)
		{
			_personsRepository = personsRepository;
			_logger = logger;
			_diagnosticContext = diagnosticContext;
			//_countryservice = countryService;
		}
		//private PersonResponse ConvertPersonToPersonResponse(Person person)
		//{
		//	PersonResponse personre = person.ToPersonResponse();
		//	personre.Country = _countryservice.GetCountryByID(person.CountryID)?.Countryname;
		//	return personre;
		//}
		public async Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest)
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

			//_db.sp_InsertPerson(person);

			 await _personsRepository.AddPerson(person);

			//person
			PersonResponse response = person.ToPersonResponse();
			//herewe want to get the country name from countryID ,so we candothis by usingthe GetCountryByID
			//function we created in CountryService
			//but you havetoinject it in your constructor to use it
			
			//response.Country = _countryservice.GetCountryByID(person.CountryID)?.Countryname;
			//if you didnt put the "?" here it will give yopu null ref error , what the "?" do here
			//it says if that _countryservice.GetCountryByID(person.CountryID) returned null , dont access
			//this countryname property , and assign null to the Country peroperty 

			return response;
		}
		public async Task<PersonResponse> GetPersonByID(Guid? personID)
		{
			if (personID==null)
			{
				return null;
			}

			Person? person = await _personsRepository.GetPersonByPersonID(personID.Value);
			if (person==null)
			{
				throw new ArgumentNullException();
			}
			PersonResponse personResponse = person.ToPersonResponse();
			return personResponse;
		}
		public async Task<List<PersonResponse>> GetAllPersons()
		{
			/*there is a problem here called Eager Execution-->if you are executing
			  _db.Persons.Select(person => ConvertPersonToPersonResponse(person)).ToList()
			//here there is an error , you cant call your own method as a part of the linq to entities expression
			you are converting each person obj to PersonResponse , butthe problem is you didnt load the
			person objects yet , so solution is call Tolist() on the db.Persons , beforethe linq expression,
			sonow you have person objects
			
			 
			 */
			//List<PersonResponse> personresponses = _db.sp_GetAllPersons()
			//	.Select(person => ConvertPersonToPersonResponse(person)).ToList();

			_logger.LogInformation("GetAllPersons from PersonsService");
			var persons = await _personsRepository.GetAllPersons();
			List<PersonResponse> personresponses =persons.Select(person => person.ToPersonResponse()).ToList();
			return personresponses;
			//List<PersonResponse> personResponses = 
		}
		public async Task<List<PersonResponse>> GetFilteredPersons(string SearchBy, string? SearchString)
		{
			/*we edited the whole codefor memory improvement , in eveery case 
			 what we did earlier that we was getting all persons then we do our 
			query , but what is better is that we do the query on the db directly
			*/
			//List<PersonResponse> ActualList = _persons.Select(p => p.ToPersonResponse()).ToList();

			//List<PersonResponse> MatchingList = ActualList;//let it = actuallist because if there is no filters , it returns the whole list



			//if (string.IsNullOrEmpty(SearchString)||string.IsNullOrEmpty(SearchBy))
			//{
			//	return MatchingList;
			//}

			//now i needto check if the searchby is really like a preperty in Person
			List<Person> ActualList = SearchBy switch
			{
				nameof(PersonResponse.PersonName) =>
					await _personsRepository.GetFilteredPersons(p =>
					p.PersonName.Contains(SearchString)),

				nameof(PersonResponse.EmailAddress) =>
					await _personsRepository.GetFilteredPersons(p =>
					p.EmailAddress.Contains(SearchString)),

				nameof(PersonResponse.DateOfBirth) =>
					await _personsRepository.GetFilteredPersons(p =>
					p.DateOfBirth.Value.ToString("dd MMM yyyy").Contains(SearchString)),

				nameof(PersonResponse.Gender) =>
					await _personsRepository.GetFilteredPersons(p =>
					p.Gender.Contains(SearchString)),

				nameof(PersonResponse.CountryID) =>
					await _personsRepository.GetFilteredPersons(p =>
					p.Country.Countryname.Contains(SearchString)),
				nameof(PersonResponse.Address) =>
					await _personsRepository.GetFilteredPersons(p =>
					p.Address.Contains(SearchString)),

				_ => await _personsRepository.GetAllPersons()
			};
			_diagnosticContext.Set("Persons", ActualList);//value can be anything
			//here we added the list of persons 
			//as a diagnostic ,  so we can see it as an additional property in the completion Log
			
			return ActualList.Select(p => p.ToPersonResponse()).ToList();
			
		}
		public async Task< List<PersonResponse>> GetSortedPersons(List<PersonResponse> allPersons, string SortBy, SortOrderEnum SortOrder)
		{

			//allPersons = GetAllPersons();
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

		public async Task<PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest)
		{
			//List<PersonResponse> AllPersons = GetAllPersons();
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

			Person? FoundedPerson=await _personsRepository.GetPersonByPersonID(personUpdateRequest.PersonID);
			if (FoundedPerson==null)
			{
				throw new ArgumentException("ID is invalid");
			}
			//here FoundedPerson is deatched obj for EF , EF is not tracking it becasuse
			//it got it from the repo , that's why we are updating again
			FoundedPerson.PersonName = personUpdateRequest.PersonName;
			FoundedPerson.Address = personUpdateRequest.Address;
			FoundedPerson.Gender = personUpdateRequest.Gender.ToString();
			FoundedPerson.ReccivenewsLetters = personUpdateRequest.ReccivenewsLetters;
			FoundedPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
			FoundedPerson.EmailAddress = personUpdateRequest.EmailAddress;
			FoundedPerson.CountryID = personUpdateRequest.CountryID;

			//await _personsRepository.SaveChangesAsync();
			await _personsRepository.UpdatePerson(FoundedPerson);

			PersonResponse Response = FoundedPerson.ToPersonResponse();

			return Response;
		}

		public async Task<bool> DeletePerson(Guid? PersonID)
		{
			if (PersonID==null)
			{
				return false;
			}
			Person? person =await _personsRepository.GetPersonByPersonID(PersonID.Value);
			if (person==null)
			{
				return false;
			}
			await _personsRepository.DeletePersonByPersonID(PersonID.Value);
			return true;
		}

		//wecreated this method to load the country

	}
}
