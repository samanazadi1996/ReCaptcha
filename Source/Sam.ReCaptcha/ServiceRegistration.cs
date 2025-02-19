using Sam.ReCaptcha.Services;
using Sam.ReCaptcha.Services.Implementations;
using System;

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
        services.AddScoped<IReCaptchaService, ReCaptchaService>();

        return services;
    }
}
