using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 3;
                }
            )

                .AddEntityFrameworkStores<PersonsDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, PersonsDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, PersonsDbContext, Guid>>();
            var app = builder.Build();
            Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot",wkhtmltopdfRelativePath:"Rotativa");
            app.UseAuthentication();//tofetch thecookie and wecan then use the @User to display username after login
            app.UseRouting();
            app.UseStaticFiles();
            app.MapControllers();


            app.Run();
        }
    }
}
