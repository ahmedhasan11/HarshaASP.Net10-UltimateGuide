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

		//public List<PersonResponse> GetAllPersons()
		//{
		//	throw new NotImplementedException();
		//}
	}
}
