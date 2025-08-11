namespace e_commerce_Task_ModelBinding
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllers();
			var app = builder.Build();

			app.UseStaticFiles();
			app.UseRouting();
			app.MapControllers();

			app.Run();
        }
    }
}
