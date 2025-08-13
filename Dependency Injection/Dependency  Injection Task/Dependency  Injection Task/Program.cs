using Dependency__Injection_Task.ServiceContract;
using Dependency__Injection_Task.Services;

namespace Dependency__Injection_Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<ICityWeather, CityWeatherService>();


            var app = builder.Build();
            app.UseRouting();
            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
