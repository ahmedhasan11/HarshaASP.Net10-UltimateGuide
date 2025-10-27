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
	public class PersonAdderService : IPersonAdderService
	{
		private readonly IPersonsRepository _personsRepository;
		private readonly ILogger<PersonGetterService> _logger;
		private readonly IDiagnosticContext _diagnosticContext;
		//private readonly ICountryService _countryservice;

		public PersonAdderService(IPersonsRepository personsRepository, ILogger<PersonGetterService> logger,IDiagnosticContext diagnosticContext /*ICountryService countryService*/)
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
	}
}
