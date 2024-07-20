using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net;

namespace CNOrderApi.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticationValidator
    {
        private readonly RequestDelegate _next;

        private const string UserContext = "UserEmail";

        public AuthenticationValidator(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// custom middleware for api key authentication
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.TryGetValue(UserContext, out var contextEmail))
            {
                httpContext.Response.StatusCode = 403;
                await httpContext.Response.WriteAsync("User email not found."); //Submit user email in header for testing
                return;
            }

            
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationValidatorExtensions
    {
        public static IApplicationBuilder UseAuthenticationValidator(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationValidator>();
        }
    }
}
