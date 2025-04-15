namespace RedirectResult_P1
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

            //when to use the Redirecttoaction ??
            //here you have old URL and in real world project if the user made a bookmark
            //for the url called /bookstore so what if you want to change that route in your project
            //andmake it /store/book ,, so here if you change it so the user if he search for his
            //url bookmark which is /bookmark will give him not found

            //solutionhere is to use the redirecttoaction , we will redirect the action we are in
            //to another action so it will have its route Url in the location url browser

            //if you check for inspect network you will find this status code is 302 
            //which is found but moved , so its moved temporarily
             
            //301---->moved permanent (use it if your code will always have this redirection)
            //the browser will replace the old url withthe new url

            //302-->moved temporarily
        }
    }
}
