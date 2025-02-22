using Sam.ReCaptcha.Services;
using Sam.ReCaptcha.Services.Implementations;
using System;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddCaptcha(this IServiceCollection services)
    {
        return services.AddCaptcha(new CaptchaOptions());
    }
    public static IServiceCollection AddCaptcha(this IServiceCollection services, Action<CaptchaOptions> configureOptions)
    {
        var options = new CaptchaOptions();
        configureOptions(options);

        return services.AddCaptcha(options);
    }
    public static IServiceCollection AddCaptcha(this IServiceCollection services, CaptchaOptions options)
    {
        services.AddSingleton(options);
        services.AddScoped<SharedServices>();

        if (options.CaptchaVariant == CaptchaTypes.DefaultCaptcha)
        {
            services.AddScoped<ICaptchaService, DefaultCaptchaService>();
        }
        else if (options.CaptchaVariant == CaptchaTypes.MathCaptcha)
        {
            services.AddScoped<ICaptchaService, MathCaptchaService>();
        }

        return services;
    }
}
