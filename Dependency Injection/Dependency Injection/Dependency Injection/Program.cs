using ServiceContract;
using Services;
namespace Dependency_Injection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            //builder.sevices is the IOC Container (Check IOC notes)
            //we have to add the service class to the IOC Container
            //you have to supply Service Details --> in a obj of ServiceDescriptor class
            //Service Details--> interface & service class & ServiceLifeTime

            /*hey service container , whenever somebody asks for ICityService -->
			 * create an object of the CityService*/

            /*example: home controller asked for an object implements ICityService ,  so the request
             will be sent to the IOC Container , it creates object of the service class,supplies
            this object to the HomeController*/
            builder.Services.Add(new ServiceDescriptor(

                typeof(ICityService),
                typeof(CityService),
                ServiceLifetime.Transient
            
                //instead ofthe above code you can do :
                //builder.services.addTransinet<Interface, service class>();


			/*what is the service lifetime?-->1-when service object should be created
			 2-whenservice object should be destroyed

			//1-when objects will be created

			(*)Transient: a newservice obj will be created everytime the service is injected
			(*)Scoped: new service object is created for every scope(Browser Request)
			(*)Singleton:service object iscreated when it's injected for the firsttime
			(the same service object will be reused every time), so new objects will not
			be createduntil the application shut down



			//2-whenservice object should be destroyed
            (*)Transient: object will be destroyed at the end ofthe scope(Browser request)
			(*)Scoped: object will be destroyed when the scope complete(Browser Request)
			(*)Singleton:object willbe destroyed when the application shutdown
			 */




			));
			var app = builder.Build();
            app.UseRouting();
            app.UseStaticFiles();
            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
