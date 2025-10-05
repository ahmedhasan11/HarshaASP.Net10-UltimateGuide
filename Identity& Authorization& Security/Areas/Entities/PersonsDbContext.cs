using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class PersonsDbContext:IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public PersonsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
		public DbSet<Person> Persons { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//FLUENT API :Table Name

			modelBuilder.Entity<Country>().ToTable("Countries");

			modelBuilder.Entity<Person>().ToTable("Persons");

			//FLUENT API :Seeding data
			string CountriesJson = System.IO.File.ReadAllText("countries.json");

			List<Country> Countries=System.Text.Json.JsonSerializer.Deserialize<List<Country>>(CountriesJson);

			foreach (Country country in Countries)
				modelBuilder.Entity<Country>().HasData(country);


			string PersonsJson=System.IO.File.ReadAllText("persons.json");
			List<Person> Persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(PersonsJson);

			foreach(Person person in Persons)
			modelBuilder.Entity<Person>().HasData(person);

			//FLUENT API : Change Col Name,DataType, add DefaultValue
			modelBuilder.Entity<Person>().Property(p => p.TIN).HasColumnName("TaxIdentificationNumber").HasColumnType("varchar(40)").HasDefaultValue("ABC12345");
			//FLUENT API : if you need the TIN column all its values to be unique
			//modelBuilder.Entity<Person>().HasIndex().IsUnique();
			//FLUENT API : Constraint for TIN
			modelBuilder.Entity<Person>().HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber])=8");


			//FLUENT API : add the Relationship Configuration (optional)
			//modelBuilder.Entity<Person>().HasOne<Country>(p => p.Country).WithMany(c => c.Persons).HasForeignKey(p => p.CountryID);




		}

		public List<Person> sp_GetAllPersons()
		{
			return Persons.FromSqlRaw("EXEC [dbo].[GetAllPersons]").ToList();
		}

		public int sp_InsertPerson(Person person)
		{
			SqlParameter[] sqlParameters = new SqlParameter[]
			{
				new SqlParameter("@PersonID",person.PersonID),
				new SqlParameter("@PersonName",person.PersonName),
				new SqlParameter("@EmailAddress",person.EmailAddress),
				new SqlParameter("@DateOfBirth",person.DateOfBirth),
				new SqlParameter("@Gender",person.Gender),
				new SqlParameter("@Address",person.Address),
				new SqlParameter("@CountryID",person.CountryID),
				new SqlParameter("@ReccivenewsLetters",person.ReccivenewsLetters)
			};

			return Database.ExecuteSqlRaw("EXEC [dbo].[InsertPerson] @PersonID ,@PersonName,@EmailAddress,@DateOfBirth,@Gender,@Address,@CountryID,@ReccivenewsLetters"
				,sqlParameters);


		}
	}
}
