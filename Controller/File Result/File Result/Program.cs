namespace File_Result
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
            app.UseStaticFiles();

            app.MapGet("/", () => "Hello World!");

            app.Run();

			#region VirtualFileResult
            //use it when your file is in the wwwroot or in a subfolder that inside the wwwroot
			#endregion

			#region PhysicalFileResult
            //use it if the file is outside the wwwroot folder
			#endregion

			#region FileContentResult
            //in real world projects sometimes you might have a file that is loaded from
            //another data source as a byte array such as reading images from databases
            //in last example you will have the images as a byte array that is raw data
			#endregion
		}
	}
}
