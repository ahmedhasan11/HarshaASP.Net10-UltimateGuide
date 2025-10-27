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
using SerilogTimings;


namespace Services
{
	public class PersonGetterService : IPersonGetterService
	{
		private readonly IPersonsRepository _personsRepository;
		private readonly ILogger<PersonGetterService> _logger;
		private readonly IDiagnosticContext _diagnosticContext;
		//private readonly ICountryService _countryservice;

		public PersonGetterService(IPersonsRepository personsRepository, ILogger<PersonGetterService> logger,IDiagnosticContext diagnosticContext /*ICountryService countryService*/)
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
			List<Person> ActualList;
			using (Operation.Time("Time of GetFilteredPersons in PersonService")) 
			{
				ActualList = SearchBy switch
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

			}


			_diagnosticContext.Set("Persons", ActualList);//value can be anything
			//here we added the list of persons 
			//as a diagnostic ,  so we can see it as an additional property in the completion Log
			
			return ActualList.Select(p => p.ToPersonResponse()).ToList();
			
		}


	}
}
