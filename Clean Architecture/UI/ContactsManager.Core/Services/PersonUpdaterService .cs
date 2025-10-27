using Entities;
using Microsoft.AspNetCore.Mvc;

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
	public class PersonUpdaterService : IPersonUpdaterService
	{
		private readonly IPersonsRepository _personsRepository;
		private readonly ILogger<PersonGetterService> _logger;
		private readonly IDiagnosticContext _diagnosticContext;
		//private readonly ICountryService _countryservice;

		public PersonUpdaterService(IPersonsRepository personsRepository, ILogger<PersonGetterService> logger,IDiagnosticContext diagnosticContext /*ICountryService countryService*/)
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

	}
}
