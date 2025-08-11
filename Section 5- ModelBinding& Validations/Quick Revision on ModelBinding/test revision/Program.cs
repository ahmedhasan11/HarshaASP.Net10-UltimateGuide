namespace test_revision
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            builder.Services.AddControllers();
            builder.Services.AddControllers().AddXmlSerializerFormatters();
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
