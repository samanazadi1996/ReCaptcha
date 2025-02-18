using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sam.ReCaptcha.Services;
using Sam.ReCaptcha.Services.implementations;
using System;
using System.Drawing;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddReCaptcha(this IServiceCollection services)
    {
        return services.AddServices(new ReCaptchaOptions());
    }
    public static IServiceCollection AddReCaptcha(this IServiceCollection services, Action<ReCaptchaOptions> configureOptions)
    {
        var options = new ReCaptchaOptions();
        configureOptions(options);

        return services.AddServices(options);
    }
    private static IServiceCollection AddServices(this IServiceCollection services, ReCaptchaOptions options)
    {
        services.AddSingleton(options);
        services.AddScoped<IImageGenerator, ImageGenerator>();

        return services;
    }
    public static IEndpointRouteBuilder UseReCaptcha(this IEndpointRouteBuilder app)
    {
        app.MapGet("/Captcha/{id:guid}", async (Guid id,  HttpContext httpContext) =>
        {
            var service = httpContext.RequestServices.GetRequiredService<IImageGenerator>();

            byte[] imageBytes = service.GetBytes("");

            httpContext.Response.ContentType = "image/png";

            await httpContext.Response.Body.WriteAsync(imageBytes, 0, imageBytes.Length);
        });

        return app;
    }

}
public class ReCaptchaOptions
{
    public string CodeCharacter { get; set; } = "0123456789";
    public Color HatchColor { get; set; } = Color.Silver;
    public Color BackColor { get; set; } = Color.White;
    public Color ForeColor { get; set; } = Color.Gray;
}
