using StocksApp_Task.Models;
using StocksApp_Task.ServiceContracts;
using StocksApp_Task.Services;

namespace StocksApp_Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IFinHubService, FinHubService>();
            builder.Services.Configure<TradingOptionsoptionpattern>(builder.Configuration.GetSection("TradingOptions"));
            var app = builder.Build();
            app.UseRouting();
            app.UseStaticFiles();
			app.MapControllers();
			app.Run();
        }
    }
}
