using Configuration.Models;
using Configuration.Services;

namespace Configuration
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            //injecting HttpClient Service
            builder.Services.AddHttpClient();
            //injecting options pattern
            builder.Services.Configure<OptionsPattern>(builder.Configuration.GetSection("weatherAPI"));

            builder.Services.AddScoped<Finhub>();
            var app = builder.Build();
            app.Run();
        }
    }
}
