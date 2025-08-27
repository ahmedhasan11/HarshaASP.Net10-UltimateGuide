using System;
using System.ComponentModel.DataAnnotations;


namespace Entities
{
   public class Person
    {
        public Guid PersonID { get; set; }

        public string? PersonName { get; set; }
        public string? EmailAddress { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? Gender { get; set; }
		public string? Address { get; set; }

		public Guid? CountryID { get; set; } //FK
		public bool? ReccivenewsLetters { get; set; }

	}
}
