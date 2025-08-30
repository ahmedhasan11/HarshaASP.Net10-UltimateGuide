using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class PersonUpdateRequest
    {
		[Required(ErrorMessage ="personid cant be blank")] //to updatespecific person the user must supply the person id 
		public Guid PersonID { get; set; }
		[Required(ErrorMessage = "Name is Required")]
		public string? PersonName { get; set; }

		[EmailAddress(ErrorMessage = "email should be in correct format")]
		[Required(ErrorMessage = "email is required")]
		public string? EmailAddress { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public GenderOptions? Gender { get; set; } //makeenum here only because here is the request properties
		public string? Address { get; set; }

		public Guid? CountryID { get; set; } //FK
		public bool? ReccivenewsLetters { get; set; }
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
				ReccivenewsLetters = ReccivenewsLetters
			};

			return person;
		}

	}
}
