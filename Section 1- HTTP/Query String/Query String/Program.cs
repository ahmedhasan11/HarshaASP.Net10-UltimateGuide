namespace Query_String
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
                context.Response.Headers["Content-Type"] = "text/html";
            if (context.Request.Method == "GET")
                {
                    if (context.Request.Query.ContainsKey("id"))
                    {
                        string id = context.Request.Query["id"];
                        await context.Response.WriteAsync($"{id}");
                    }
                }
            });
        }
    }
}
