using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
	/// <summary>
	/// DTO used as a return type of most methods of persons service 
	/// </summary>
	/// 

	//calculated properties can be done in service or here in the extension method below
	public class PersonResponse
    {
		public Guid PersonID { get; set; }
		public string? PersonName { get; set; }
		public string? EmailAddress { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public double? Age { get; set; } //we would like to retrieve age based on DateOfBirth(do it in service )
		public string? Gender { get; set; }
		public string? Address { get; set; }

		public Guid? CountryID { get; set; } //FK
		public string? Country { get; set; }//we would like to retrieve the country name based on CountryID(do it in service)
		public bool? ReccivenewsLetters { get; set; }

		//we will also need the equals override method as we did in thecountry response class, because
		//we will need to use the Assert.Contains or Assert.Equals in our UnitTests

		/// <summary>
		/// personresponse obj tocompare --.checking if all person details are matched with all obj details
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object? obj)
		{
			if (obj==null)
			{
				return false;
			}
			if (obj.GetType()!=typeof(PersonResponse))
			{
				return false;				
			}

			PersonResponse personResponse = (PersonResponse)obj;

			return PersonName == personResponse.PersonName && Gender == personResponse.Gender && EmailAddress == personResponse.EmailAddress &&
				DateOfBirth == personResponse.DateOfBirth && CountryID == personResponse.CountryID && ReccivenewsLetters == personResponse.ReccivenewsLetters &&
				Address == personResponse.Address;

		}

		public override string ToString()
		{
			return ($"person id:{PersonID}, personname:{PersonName}" );
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}
		public PersonUpdateRequest ToPersonUpdateRequest()
		{
			return new PersonUpdateRequest()
			{
				PersonID = PersonID,
				PersonName = PersonName,
				//in PersonResponse gdner is string type so you have to coonvert it into Enum
				Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true),
				DateOfBirth = DateOfBirth,
				Address = Address,
				EmailAddress = EmailAddress,
				CountryID = CountryID,
				ReccivenewsLetters = ReccivenewsLetters,			
			};
		}

	}
	//calculated properties can be done in service or here in the extension method below
	public static class PersonExtensions
	{
		//injecting this method intopersonclass without modifying the code of the Person Class 
		//Sol:inject a new method

		/// <summary>
		/// extensionmethod to convert an obj ofpersonclassinto personresponse class
		/// </summary>
		/// <param name="person"></param>
		/// <returns>returns the converted personresponse</returns>
		public static PersonResponse ToPersonResponse(this Person person)
		{
			return new PersonResponse()
			{
				PersonID = person.PersonID,
				PersonName = person.PersonName,
				Gender = person.Gender,
				DateOfBirth = person.DateOfBirth,
				Address = person.Address,
				EmailAddress = person.EmailAddress,
				CountryID = person.CountryID,
				ReccivenewsLetters = person.ReccivenewsLetters,
				Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25): null};
			//	//calculated properties can be done in service or here in the extension method below
			//like we calculated the age
		}

		}
	}

	

