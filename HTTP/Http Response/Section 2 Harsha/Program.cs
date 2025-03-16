namespace Section_2_Harsha
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run(async(HttpContext context) => 
            {
                if (true)
                {
					context.Response.StatusCode = 200;
				}
                else 
                {

					context.Response.StatusCode = 400;
				}
                context.Response.Headers["MyKey"]="my value";
				context.Response.Headers["Server"] = "My Server";
				context.Response.Headers["Content-Type"] = "text/html";

				await context.Response.WriteAsync("Hello");
				await context.Response.WriteAsync("World");

			});
        }
    }
}
