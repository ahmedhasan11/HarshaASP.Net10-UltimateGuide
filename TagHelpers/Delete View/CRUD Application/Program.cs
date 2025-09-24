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
			builder.Services.AddSingleton<ICountryService, CountryService>();//ineedto use countryservice
            //ontime in the entireapplicationuntilyou shut downthe app by closing the kestrel
			builder.Services.AddSingleton<IPersonService, PersonService>();
            var app = builder.Build();

            app.UseRouting();
            app.UseStaticFiles();
            app.MapControllers();


            app.Run();
        }
    }
}
