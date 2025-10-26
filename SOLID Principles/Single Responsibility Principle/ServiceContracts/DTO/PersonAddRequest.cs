using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceContracts.Enums;
namespace ServiceContracts.DTO
{
	/// <summary>
	/// here you put the propertiesthat you want the user to add , that's why we separatethe model by making DTO's
	/// so Model:contain all properties thatyou want to store in DB table
	/// DTO:properties that enetered by the user
	/// 
	/// 
	/// DTO forinserting a newperson
	/// </summary>
    public class PersonAddRequest
    {
		[Required(ErrorMessage ="Name is Required")]
		public string? PersonName { get; set; }

		[EmailAddress(ErrorMessage ="email should be in correct format")]
		[Required(ErrorMessage ="email is required")]
		[DataType(DataType.EmailAddress)]
		public string? EmailAddress { get; set; }
		[DataType(DataType.Date)]
		public DateTime? DateOfBirth { get; set; }
		public GenderOptions? Gender { get; set; } //makeenum here only because here is the request properties
		public string? Address { get; set; }

		public Guid? CountryID { get; set; } //FK
		public bool ReccivenewsLetters { get; set; }
		public Person ToPerson()
		{
			Person person = new Person()
			{
				PersonName = PersonName,
				Address = Address,
				CountryID = CountryID,
				DateOfBirth = DateOfBirth,
				Gender = Gender.ToString(),
				EmailAddress = EmailAddress,
				ReccivenewsLetters=ReccivenewsLetters
			};

			return person;
		}
	}
}
