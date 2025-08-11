namespace Routing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

			#region Routing & EndPoints
			////when we use routing we have 2 codes
			////app.UseRouting();
			//app.UseRouting();
			////app.UseEndpoints(); -->takes lambda expression as a parameter 


			//app.UseEndpoints
			//(endpoints =>
			//{
			//	//endpoints.Map() takes the url as first parameter , second parameter is middleware
			//	endpoints.Map("/map1", async (context) =>
			//	{
			//		await context.Response.WriteAsync("InMap1");
			//	});
			//	endpoints.MapGet("/map2", async (context) =>
			//	{
			//		await context.Response.WriteAsync("InMap2");
			//	});
			//	endpoints.MapPost("/map3", async (context) =>
			//	{
			//		await context.Response.WriteAsync("InMap3");
			//	});
			//}
			//	);

			////what happened above that if tha path is /map1 so the endpoint 1 will be executed
			////if path is /map2 so endpoint 2 will be executed
			////last 2 sentences will be happened whateverthe method getor postor anything used
			////if youwant the sentence to me completed only with get method so use .MapGet
			////if youwant the sentence to me completed only with Post method so use .MapPost 
			#endregion

			#region GetEndPoint 
			////GetEndPoint: is a method you can access 
			////it will help you know which endpoint is used 
			////it have properties like DisplayName that give you name of the EndPoint used
			////this should be used after app.UseRouting() because the process is
			////1-app.UseRouting() is checking the request you got
			////2- app.UseRouting determines best endpoint that matching the request or URL
			////3-after that the GetEndPoint can know which EndPoint is used


			////Problems??
			////1- if you have run the GetEndPoint before app.UseRouting();
			////2- so the GetEndPoint will return NULL

		

			//app.UseRouting();

			//app.Use(async (context, next) =>
			//{
			//	Microsoft.AspNetCore.Http.Endpoint gettingendpoint = context.GetEndpoint();
			//	await next(context);
			//});


			//app.UseEndpoints(endpoint=>
			//{
			//	endpoint.MapGet("Map1" , async (context) =>
			//	{
			//		await context.Response.WriteAsync("");
			//	});

			//	endpoint.MapPost("Map2", async(context) =>
			//	{
			//		await context.Response.WriteAsync("");
			//	});
			//});
			#endregion

			app.Run();
        }
    }
}
