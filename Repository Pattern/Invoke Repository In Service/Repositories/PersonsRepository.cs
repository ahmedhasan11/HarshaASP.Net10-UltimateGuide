using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
	public class PersonsRepository : IPersonsRepository
	{
		private readonly PersonsDbContext _context;
		public PersonsRepository(PersonsDbContext context)
		{
			_context = context;
		}
		public async Task<Person> AddPerson(Person person)
		{
			_context.Persons.Add(person);
			await _context.SaveChangesAsync();
			return person;
		}

		public async Task<bool> DeletePersonByPersonID(Guid personID)
		{
			_context.Persons.RemoveRange(_context.Persons.Where(p => p.PersonID == personID));
			int rowsDeleted= await _context.SaveChangesAsync();
			return rowsDeleted>0;
		}
		public async Task<List<Person>> GetAllPersons()
		{
			return await _context.Persons.Include(p=>p.Country).ToListAsync();
		}
		public async Task<Person?> GetPersonByPersonID(Guid personID)
		{
			return await _context.Persons.Include(p=>p.Country).FirstOrDefaultAsync(p => p.PersonID == personID);
		}


		public async Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate)
		{
			return await _context.Persons.Include(p => p.Country)
				.Where(predicate).ToListAsync();
			/*where(predicate) -->whatever lambda expression in the
			parameter will get applied successfully to the query*/
		}

		public async Task<Person> UpdatePerson(Person person)
		{
			Person? ActualPerson= await _context.Persons.FirstOrDefaultAsync(p => p.PersonID == person.PersonID);
			if (ActualPerson==null)
			{
				return person;
			}
			ActualPerson.PersonName = person.PersonName;
			ActualPerson.EmailAddress = person.EmailAddress;
			ActualPerson.DateOfBirth = person.DateOfBirth;
			ActualPerson.CountryID = person.CountryID;
			ActualPerson.ReccivenewsLetters = person.ReccivenewsLetters;
			ActualPerson.Gender = person.Gender;
			ActualPerson.Address = person.Address;
			int countUpdated= await _context.SaveChangesAsync();
			return ActualPerson;
		}
	}
}
