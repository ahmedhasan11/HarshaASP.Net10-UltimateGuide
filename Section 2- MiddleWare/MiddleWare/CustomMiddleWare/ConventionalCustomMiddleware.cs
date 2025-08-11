using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MiddleWare.CustomMiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ConventionalCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public ConventionalCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    //here he made the extension method ,, not like the section in the course called custom middleware extensions
    //where we made the extension method by ourselves
    public static class ConventionalCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseConventionalCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ConventionalCustomMiddleware>();
        }
    }
}
