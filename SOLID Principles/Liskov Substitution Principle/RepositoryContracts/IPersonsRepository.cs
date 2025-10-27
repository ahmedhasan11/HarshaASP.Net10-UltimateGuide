using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
	public interface IPersonsRepository
	{
		Task<Person> AddPerson(Person person);
		Task<List<Person>> GetAllPersons();

		Task<Person?> GetPersonByPersonID(Guid personID);
		/// <summary>
		/// Returns all person objects based on the given expression
		/// </summary>
		/// <param name="predicate"> LINQ expression to check</param>
		/// <returns></returns>

		/*in the parameter of GetFilteredPersons(), we will recieve a lambda 
		 expression and we will return all the elements that are matching with that
		lambda expression condition

		in brief: at the time of calling you can write your lambda expression that
		contains your condition
		argument type is person means it recieves the each person from the list
		and returns a boolean whether its matching or not
		*/
		Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate);
		Task<bool> DeletePersonByPersonID(Guid personID);
		Task<Person> UpdatePerson( Person person);
	}
}
