using System;
using System.ComponentModel.DataAnnotations;


namespace Entities
{
   public class Person
    {
		[Key]
        public Guid PersonID { get; set; }

		[StringLength(40)]
        public string? PersonName { get; set; }
		[StringLength(40)]
		public string? EmailAddress { get; set; }
		public DateTime? DateOfBirth { get; set; }
		[StringLength(10)]
		public string? Gender { get; set; }
		[StringLength(300)]
		public string? Address { get; set; }

		public Guid? CountryID { get; set; } //FK
		public bool? ReccivenewsLetters { get; set; }

		public string? TIN { get; set; }

	}
}
