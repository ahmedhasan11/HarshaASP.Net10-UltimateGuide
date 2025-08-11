namespace input_formatters
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            builder.Services.AddControllers().AddXmlSerializerFormatters();
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
