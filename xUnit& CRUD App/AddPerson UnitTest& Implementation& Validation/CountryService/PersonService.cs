using Entities;
using Service.Helpers;
using ServiceContracts;
using ServiceContracts.DTO;
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
		private List<Person> _persons;
		private ICountryService _countryservice;

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
			PersonResponse response = person.ToPersonResponse();
			//herewe want to get the country name from countryID ,so we candothis by usingthe GetCountryByID
			//function we created in CountryService
			//but you havetoinject it in your constructor to use it
			response.Country = _countryservice.GetCountryByID(person.CountryID).Countryname;

			return response;
		}

		//public List<PersonResponse> GetAllPersons()
		//{
		//	throw new NotImplementedException();
		//}
	}
}
