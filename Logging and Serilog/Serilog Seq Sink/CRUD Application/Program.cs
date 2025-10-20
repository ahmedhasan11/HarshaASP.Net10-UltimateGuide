using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Services;
using ServiceContracts;
using RepositoryContracts;
using Repositories;
using ServiceContracts.DTO;
using System.Runtime;
using Serilog;
using Serilog.AspNetCore;
namespace CRUD_Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider service, LoggerConfiguration logger_configuration) =>
            {
                logger_configuration.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(service);
            });
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            /*its not goodto inject a scoped service into singleton service*/
            /*we are injecting the personsdbcontextwhich is scoped , inside the country service that is alreadysingleton*/
            /*how personsDbContext is scoped? by default scoped*/
			builder.Services.AddScoped<ICountryService, CountryService>();//ineedto use countryservice
            //ontime in the entireapplicationuntilyou shut downthe app by closing the kestrel
			builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IPersonsRepository, PersonsRepository>();
			builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();

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

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .Build(); //your policy is whatever action methods user should be logged in
                //(the request should have the identity cookie)

                options.AddPolicy("UnauthorizedAccess", policy => 
                {
                    //here we can return either true or false , 
                    //true-->user have access
                    //false-->denied access
                    //what we need is that if the user is authenticated ,he get access denied onloginand register 
                    policy.RequireAssertion(//means you would like to check your condition
                        context => { return !context.User.Identity.IsAuthenticated; }
                        );
                });
            });
            builder.Services.ConfigureApplicationCookie(options =>
            {//whenever the abovepolicyisnot respected by the request& te user have not logged in
                //then redirect him to this url
                options.LoginPath = "/Account/Login";
            });

            builder.Services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders
                    |
                    Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;

            });
            var app = builder.Build();
            app.UseHttpLogging();
            Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot",wkhtmltopdfRelativePath:"Rotativa");
            app.UseHsts();
            app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();//tofetch thecookie and wecan then use the @User to display username after login
            app.UseAuthorization();//-->addsauthorization middleware to the request pipeline which addsthe access permissionsof theparticularuser-->it evaluateswhich the currentuser can accessthisparticularresourse or not
            app.MapControllers();
            app.UseEndpoints(endpoints =>
            {
				//conventional routing for Areas
				endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}"); //Admin/Home/Index,,,,,,,,,area here means area name


                //conventional routing for all controllers Folder
                endpoints.MapControllerRoute(
                    name: "default",
					pattern: "{controller}/{action}/{id?}");



			});

            app.Run();
        }
    }
}
