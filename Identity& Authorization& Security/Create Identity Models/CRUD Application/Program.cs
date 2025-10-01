using Entities;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Service;
using ServiceContracts;
namespace CRUD_Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            /*its not goodto inject a scoped service into singleton service*/
            /*we are injecting the personsdbcontextwhich is scoped , inside the country service that is alreadysingleton*/
            /*how personsDbContext is scoped? by default scoped*/
			builder.Services.AddScoped<ICountryService, CountryService>();//ineedto use countryservice
            //ontime in the entireapplicationuntilyou shut downthe app by closing the kestrel
			builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddDbContext<PersonsDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnectionString"]));
            var app = builder.Build();
            Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot",wkhtmltopdfRelativePath:"Rotativa");
            app.UseRouting();
            app.UseStaticFiles();
            app.MapControllers();


            app.Run();
        }
    }
}
