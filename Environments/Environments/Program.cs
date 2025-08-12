namespace Environments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

			//app.Environment.IsDevelopment()-->returns true if thisn property in launchsettings.json is Development
			//app.Environment.IsEnvironment("write here the environment name you want to check");
			// app.Environment.EnvironmentName -->returns the environment name from the launchsettings.json
			//IsDevelopment , IsStaging, IsProduction
			if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseStaticFiles();
            app.MapControllers();
            app.Run();
        }
    }
}
