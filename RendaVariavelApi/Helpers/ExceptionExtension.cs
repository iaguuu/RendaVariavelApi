using Microsoft.AspNetCore.Diagnostics;
using RendaVariavelApi.Models;

namespace RendaVariavelApi.Helpers
{
    public static class ExceptionExtension
    {
        public static void ExceptionHandling(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (errorFeature is not null)
                    {
                        var logger = loggerFactory.CreateLogger("Global exception logger");
                        logger.LogError(500, errorFeature.Error, errorFeature.Error.Message);
                        
                        await context.Response.WriteAsJsonAsync(new Result<string>
                        {
                            title = "Internal server error",
                            status = context.Response.StatusCode,
                            detail = string.IsNullOrEmpty(errorFeature.Error.Message) ? "Internal server error" : errorFeature.Error.Message,
                            result = "Error"
                        });
                    }                    
                });
            });
        }
    }
}
