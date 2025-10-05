using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
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

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .Build(); //your policy is whatever action methods user should be logged in
                //(the request should have the identity cookie)
            });
            builder.Services.ConfigureApplicationCookie(options =>
            {//whenever the abovepolicyisnot respected by the request& te user have not logged in
                //then redirect him to this url
                options.LoginPath = "/Account/Login";
            });
            var app = builder.Build();
            Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot",wkhtmltopdfRelativePath:"Rotativa");
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
