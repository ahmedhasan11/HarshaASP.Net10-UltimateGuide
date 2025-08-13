using Configuration_Task.Models;

namespace Configuration_Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<SocialLinks>(builder.Configuration.GetSection("SocialMediaLinks"));
            var app = builder.Build();

            app.MapControllers();
            app.UseRouting();
            app.UseStaticFiles();

            app.Run();
        }
    }
}
