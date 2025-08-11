namespace RouteParameters
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var app = builder.Build();

			#region Route Parameters
			////what is RouteParameters?
			////there is always part of Url that is fixed , while we was in controller sometimes 
			////if you are working in only 1 controllers name so always the url is Employee/
			////Employee/ here is the fixed part in the URL


			////sometimes you need to make parameters in the URl this parameters called --> RouteParameters
			//         //they are stored in a method calledcontext.Request.Routevalues["parametername"]

			////we can make that so we will put the parameters we want to pass in {} in the URl
			////endpoint.Map("files/{filename}.{extension}"
			//         //in the above code filename is a parameter && extension is a parameter too
			//         //you can access them because they are in the request 
			//         //

			//app.UseRouting();

			//         app.UseEndpoints(endpoint =>
			//         {
			//             endpoint.Map("files/{filename}.{extension}", async context =>
			//             {
			//                 string? FileName = Convert.ToString(context.Request.RouteValues["filename"]);
			//                 string? Extension = Convert.ToString(context.Request.RouteValues["extension"]);
			//             });


			//             endpoint.Map("Employee/Profile/{name}", async context =>
			//             {
			//                 string? Name = Convert.ToString(context.Request.RouteValues["name"]);
			//             }

			//                 );
			//         }); 
			#endregion

			#region Default Route Parameters

			//you can give a default value for the parameter in case that no value is added from the request
			// only when you make the endpoint instead of making files/{filename}.{extension}
			//you can make a default name for the filename parameter by doing {filename=1}
			//app.UseRouting();
			//app.UseEndpoints(endpoint =>
			//{
			//	endpoint.Map("files/{filename=1}.{extension}", async context =>
			//	{
			//		string? filename = Convert.ToString(context.Request.RouteValues["filename"]);
			//		string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
			//	});
			//});

			#endregion

			#region Optional Route Parameters

			//in optional parameter you should just put ? behind the parameter name
			app.UseRouting();
			app.UseEndpoints(endpoint =>
			{
				endpoint.Map("files/{filename?}.{extension}", async context =>
				{
					string? filename = Convert.ToString(context.Request.RouteValues["filename"]);
					string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
				});
			});
			#endregion

			app.Run();

		}
	}
}
