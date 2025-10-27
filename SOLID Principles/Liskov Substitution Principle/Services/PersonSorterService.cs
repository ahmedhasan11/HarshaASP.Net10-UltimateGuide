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
	public class PersonSorterService : IPersonSorterService
	{
		private readonly IPersonsRepository _personsRepository;
		private readonly ILogger<PersonGetterService> _logger;
		private readonly IDiagnosticContext _diagnosticContext;
		//private readonly ICountryService _countryservice;

		public PersonSorterService(IPersonsRepository personsRepository, ILogger<PersonGetterService> logger,IDiagnosticContext diagnosticContext /*ICountryService countryService*/)
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
	
	}
}
