using System.ComponentModel.DataAnnotations;

namespace CustomModelBinder.Models
{
	public class Person
	{
		[Required(ErrorMessage = "custom msg")]
		//[RegularExpression("^[A-Za-z .]$")] //specify some values that only accepted
		public string? PersonName { get; set; }

		[EmailAddress]
		public string? Email { get; set; }
		[Phone]
		public string? Phone { get; set; }
		[Required]
		public string? Password { get; set; }

		[Required]
		[Compare("Password")] //password and confirm password should be the same
		public string? ConfirmPassword { get; set; }
		public double? Price { get; set; }


		//public DateTime DateBirth { get; set; }
		//public DateTime from_date { get; set; }
		//public DateTime to_date { get; set; }

		#region how to recieve multiple values
		//what if i want to recieve multiple values of hash tage for example
		//make list property
		//in postman send your data with indexes
		//Tags[0]=......
		//Tags[1]=......
		#endregion
		public List<string> Tags { get; set; } = new List<string>();
		public override string ToString()
		{
			return $"Person object - Person name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
		}
	}
}
