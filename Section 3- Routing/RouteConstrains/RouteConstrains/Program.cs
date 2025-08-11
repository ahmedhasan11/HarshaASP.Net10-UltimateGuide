using Microsoft.AspNetCore.Builder;
using RouteConstrains.CustomConstraints;

namespace RouteConstrains
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
			//adding custom constraint
			builder.Services.AddRouting(options => options.ConstraintMap.Add("months", typeof(MonthsCustomConstraint)));

			//Routing Constrains is some validations that you want to add
			//an example is if you are making a parameter called id ,, what if someone instead of giving you a number
			//he gave you "ahmed" so this should not happen 

			//solution is to make validation on this id parameter
			//{parametername:constraintname}
			//so {id:int}
			//this not working with all data types
			//you can make datetime too
			//{parametername:Datetime}

			#region int & Datetime constrains
			//app.UseRouting();
			//app.UseEndpoints(endpoints =>
			//{
			//    endpoints.Map("Employees/Profile/{id:int}", async (context) =>
			//    {
			//        await context.Response.WriteAsync("");
			//      //  await next(context);
			//    });

			//    endpoints.Map("Years/{assigndate:datetime}", async context =>
			//    {
			//        await context.Response.WriteAsync("");
			//    });
			//}); 
			#endregion

			#region guid constraint
			//what is guid?
			//the hexadecimal number that is almost unique ,its unlimited
			//in real world we use it as id because its always unique , unlimited
			//how to test it , from tools choose create guid and copy one of them 
			//and give it to the request 

			//app.UseRouting();
			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.Map("cities/{cityid:guid}", async (context) =>
			//	{
			//		Guid cityid = Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"])) ;
			//		await context.Response.WriteAsync($"{cityid}");
			//		//  await next(context);
			//	});
			//});
			#endregion

			#region regex, minlength(), maxlength()

			//app.UseRouting();
			//app.UseEndpoints(endpoints =>
			//{
			//	//what is regex??
			//	//if you want the input of the request is a list of some values that only works
			//	//syntax ---> regex(^( | | | | | | )$)


			//	endpoints.Map("cities/{cityname:regex(^(alexandria|dakahliya|suhaj|dumyat)$)}", async (context) =>
			//	{

			//	});

			//	endpoints.Map("Employees/{Name:minlength(5)}", async context =>
			//	{
			//		await context.Response.WriteAsync("");
			//	});

			//	endpoints.Map("Managers/{Name:maxlength(12)}", async context =>
			//	{
			//		await context.Response.WriteAsync("");
			//	});

			//	//what if you want to add more than 1 constrain in the same time
			//	//{d:int:max(1000)}




			//	endpoints.Map("Managers/{Name:maxlength(12):minlength(1)}", async context =>
			//	{
			//		await context.Response.WriteAsync("");
			//	});
			//});
			#endregion

			#region CustomConstraint Run
			app.UseRouting();
			app.UseEndpoints(endpoints=>

			endpoints.Map("sales-report/{year:int:min(1900)}/{month:months}}", async (context) =>
			{

			})
			);


			#endregion


			app.Run();
        }
    }
}
