using MiddleWare.CustomMiddleWare;
namespace MiddleWare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
		//	builder.Services.AddTransient<MyCustomMiddleWare>();
            var app = builder.Build(); //app here is used to create middlewares

			#region Middleware using app.Run: Problems
			//app.Run(async (HttpContext context) =>
			//{
			//	await context.Response.WriteAsync("Hello");
			//});



			//see what happened , we made a request to the browser, after that middleware executed
			//so this lambda expression above is called middelware
			//another name is request delegate

			//what if we made the same code above???




			//app.Run(async (HttpContext context) =>
			//{
			//	await context.Response.WriteAsync("Hello again");
			//});


			//only hello will be print , but why?
			//this depends on the metod you use ,app.run method doesn't forward request
			//to the subsequent middleware whatever present next to that
			//so problem here is only because of app.run 
			#endregion

			//but by default the goal of middleware is to pass te request to the subsequent middlewares
			//so by default when a request is happen , the first middleware executes
			//then request is passed to te second middleware , and so on

			#region Solution of app.Run method problem : using app.Use & app.Run


			//app.Use takes 2 parameters 
			//1st one--> HttpContexnt context 
			//2nd one--> RequestDelegate next --> this parameter represents whatever subsequent middleware
			//that are present here after the above statement




			////MiddleWare 1
			//app.Use(async (HttpContext context, RequestDelegate next) => 
			//{
			//	await context.Response.WriteAsync("hello");
			//	await next(context);
			//	//calling next middleware and pass the current context as argument
			//});




			////MiddleWare 2
			//app.Use(async (HttpContext context, RequestDelegate next) =>
			//{
			//	await context.Response.WriteAsync("hello again");
			//	await next(context);
			//	//calling next middleware and pass the current context as argument
			//});





			////MiddleWare 3 --> we call it the terminating middleware (dont have next request delegate)
			////you can also make app.Use middleware is also terminating by deleting await next(context);
			//app.Run(async (HttpContext context) =>
			//{
			//	await context.Response.WriteAsync("hello  3");
			//});

			#endregion

			#region Custom Middleware
			////MiddleWare 1
			//app.Use(async (HttpContext context, RequestDelegate next) =>
			//{
			//	await context.Response.WriteAsync("hello");
			//	await next(context);

			//});

			////MiddleWare 2 : be careful that not best one to call the middleware like this
			////but the best way is to call the extension metod of this middleware

			//app.UseMiddleware<MyCustomMiddleWare>(); //use app.UseMiddleware instead of app.Use
			////when the request happened and middleware 1 executes , then next code will execute
			////then it wil go into the definition of the MyCustomMiddleWare.cs and execute the code into it


			////you can also make app.Use middleware is also terminating by deleting await next(context);
			//app.Run(async (HttpContext context) =>
			//{
			//	await context.Response.WriteAsync("hello  3");
			//});
			#endregion

			#region Conventional Middleware
			//create new item in the project and select Middleware class
			//automatically it creates for you Constructor && Invoke method && extension method 

			//calling of extension method of conventional middleware class
			//app.UseConventionalCustomMiddleware();
			#endregion

			#region UseWhen

			//app.UseWhen is used when you want the set of middlewares to execute under certain condition
			//HttpContext context;
			app.UseWhen(
				context => context.Request.Query.ContainsKey("username"),
				app => {
					app.Use(async (context, next) =>
					{
						await context.Response.WriteAsync("Hello from Middleware branch");
						await next();
					});
				});
			#endregion
			app.Run(async context =>
			{
				await context.Response.WriteAsync("Hello from middleware at main chain");
			});


		}
    }
}
