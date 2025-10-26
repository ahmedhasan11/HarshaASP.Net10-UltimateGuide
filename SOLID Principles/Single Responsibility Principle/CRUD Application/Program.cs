using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Services;
using ServiceContracts;
using RepositoryContracts;
using Repositories;
using ServiceContracts.DTO;
using System.Runtime;
using Serilog;
using Serilog.AspNetCore;
using CRUD_Application.Filters.ActionFilters;
using CRUD_Application.StartupExtensions;
using CRUD_Application.Middlewares;
namespace CRUD_Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider service, LoggerConfiguration logger_configuration) =>
            {
                logger_configuration.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(service);
            });
            //Extension method for all IOC container Services for clean code
            builder.Services.ConfigureServices(builder.Configuration);
   
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseExceptionHandlerMiddleware();
            }
                app.UseSerilogRequestLogging();//to add the completion log //look at IDiagnosticContext
            app.UseHttpLogging();
            Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot",wkhtmltopdfRelativePath:"Rotativa");
            app.UseHsts();
            app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();//tofetch thecookie and wecan then use the @User to display username after login
            app.UseAuthorization();//-->addsauthorization middleware to the request pipeline which addsthe access permissionsof theparticularuser-->it evaluateswhich the currentuser can accessthisparticularresourse or not
            app.MapControllers();
            app.UseEndpoints(endpoints =>
            {
				//conventional routing for Areas
				endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}"); //Admin/Home/Index,,,,,,,,,area here means area name


                //conventional routing for all controllers Folder
                endpoints.MapControllerRoute(
                    name: "default",
					pattern: "{controller}/{action}/{id?}");



			});

            app.Run();
        }
    }
}
