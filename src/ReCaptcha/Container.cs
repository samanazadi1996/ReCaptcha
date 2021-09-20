using Microsoft.Extensions.DependencyInjection;
using ReCaptcha.Services;

namespace ReCaptcha
{
    public static class Container
    {
        public static IServiceCollection AddReCaptchaServices(this IServiceCollection services)
        {
            services.AddSingleton<ICodeGenerator, CodeGenerator>();
            services.AddSingleton<IImageGenerator, ImageGenerator>();
            return services;
        }
    }

}
