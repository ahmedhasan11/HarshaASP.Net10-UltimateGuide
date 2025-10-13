using CitiesManager.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CitiesManager.Web.DatabaseContext
{
	public class CitiesDbContext : DbContext
	{
		public CitiesDbContext(DbContextOptions options):base(options)
		{

		}

		public CitiesDbContext()
		{

		}
		public DbSet<City> Cities{get;set;}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<City>().HasData(new City() { CityID= Guid.Parse("{21D68AA7-7214-4194-9662-95703EA61D4B}"), CityName="New York"});
			modelBuilder.Entity<City>().HasData(new City() { CityID = Guid.Parse("{92CB40DB-A930-482F-84C0-4D1538A01CC4}"), CityName = "London" });
		}
	}
}
