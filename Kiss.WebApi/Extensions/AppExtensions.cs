using AutoWrapper;
using Microsoft.AspNetCore.Builder;

namespace Kiss.Api.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            // Enable middle-ware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middle-ware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kiss.WebApi");
            });
        }

        public static void UseAutoWraperExtension(this IApplicationBuilder app)
        {
            //Enable AutoWrapper.Core
            //More info see: https://github.com/proudmonkey/AutoWrapper
            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { IsDebug = true, UseApiProblemDetailsException = true });
        }
    }
}
