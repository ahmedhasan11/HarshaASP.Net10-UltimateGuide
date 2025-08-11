namespace Query_String___Route_Parameters
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

            //what is ModelBinding used for??
            //model binding taking the inputs from the request and it be executed before the action 
            //is executed ,,you can imagineit as a intermedititate layer between request and action that
            //request is routed to 
            //so it fetches input from request and pass it's values to the action
            //whatever the values is route values or passed in the query string(Url)

//##check the controller classes 
        }
    }
}
