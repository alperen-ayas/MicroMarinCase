using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using System.Text;
using System.Text.Json;

namespace MicroMarinCase.Api.Middlewares
{
    public static class ValidationExceptionMiddleware
    {
        public static void UseCatchValidationError(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    if (!(exception is ValidationException validationException))
                    {
                        var errorMessage = JsonSerializer.Serialize(exception.Message);
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(errorMessage, Encoding.UTF8);

                    }
                    else
                    {
                        Dictionary<string, string> errors = new Dictionary<string, string>();

                    foreach (var error in validationException.Errors)
                    {
                        errors.Add(error.PropertyName, error.ErrorMessage);
                    }

                    var errorText = JsonSerializer.Serialize(errors);
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    errorText = JsonSerializer.Serialize(errors);
                    await context.Response.WriteAsync(errorText, Encoding.UTF8);
                    }

                    


                });
            });
        }
    }
}
