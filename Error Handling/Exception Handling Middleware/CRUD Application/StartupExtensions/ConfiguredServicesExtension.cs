using CRUD_Application.Filters.ActionFilters;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;

namespace CRUD_Application.StartupExtensions
{
	//we made it static because the static method inside astatic class can be 
	//configured as an extension method
	public static class ConfiguredServicesExtension
	{
		public static IServiceCollection ConfigureServices(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddControllersWithViews(options =>
			{
				//good if this filter dont have parameters
				//options.Filters.Add<ResponseHeaderActionFilter>(); 

				//getting the loger so we can pass it to the constructor of FilterClass
				var logger = services.BuildServiceProvider().GetRequiredService<ILogger<ResponseHeaderActionFilter>>();

				options.Filters.Add(new ResponseHeaderActionFilter(logger, "custom-key-from-Global", "custom-val-from-Global"));

				options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
			});
			/*its not goodto inject a scoped service into singleton service*/
			/*we are injecting the personsdbcontextwhich is scoped , inside the country service that is alreadysingleton*/
			/*how personsDbContext is scoped? by default scoped*/
			services.AddScoped<ICountryService, CountryService>();//ineedto use countryservice
																  //ontime in the entireapplicationuntilyou shut downthe app by closing the kestrel
			services.AddScoped<IPersonService, PersonService>();
			services.AddScoped<IPersonsRepository, PersonsRepository>();
			services.AddScoped<ICountriesRepository, CountriesRepository>();

			services.AddDbContext<PersonsDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:DefaultConnectionString"]));
			services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
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

			services.AddAuthorization(options =>
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
			services.ConfigureApplicationCookie(options =>
			{//whenever the abovepolicyisnot respected by the request& te user have not logged in
			 //then redirect him to this url
				options.LoginPath = "/Account/Login";
			});

			services.AddHttpLogging(options =>
			{
				options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders
					|
					Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;

			});

			return services;
		}
	}
}
