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
	public class PersonDeleterService : IPersonDeleterService
	{
		private readonly IPersonsRepository _personsRepository;
		private readonly ILogger<PersonGetterService> _logger;
		private readonly IDiagnosticContext _diagnosticContext;
		//private readonly ICountryService _countryservice;

		public PersonDeleterService(IPersonsRepository personsRepository, ILogger<PersonGetterService> logger,IDiagnosticContext diagnosticContext /*ICountryService countryService*/)
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
