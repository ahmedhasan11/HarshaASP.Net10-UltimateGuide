namespace Content_Result
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
			builder.Services.AddControllers();
			app.UseRouting();
			app.MapControllers();

			app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
