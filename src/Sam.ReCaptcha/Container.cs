using Microsoft.Extensions.DependencyInjection;
using Sam.ReCaptcha.Services;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sam.ReCaptcha
{
    public static class Container
    {
        public static IServiceCollection AddReCaptchaServices(this IServiceCollection services)
        {
            return services.AddServices(new ReCaptchaOptions());
        }
        public static IServiceCollection AddReCaptchaServices(this IServiceCollection services, Action<ReCaptchaOptions> configureOptions)
        {
            ReCaptchaOptions options = new ReCaptchaOptions();
            configureOptions(options);
            return services.AddServices(options);
        }
        private static IServiceCollection AddServices(this IServiceCollection services, ReCaptchaOptions options)
        {
            services.AddSingleton(options);
            services.AddSingleton<ICodeGenerator, CodeGenerator>();
            services.AddSingleton<IImageGenerator, ImageGenerator>();
            return services;
        }
    }
    public class ReCaptchaOptions
    {
        public string CodeCharacter { get; set; } = "0123456789";
        public int CodeLength { get; set; } = 4;
        public HatchStyle Hatchstyle { get; set; } = HatchStyle.Cross;
        public Color HatchColor { get; set; } = Color.Silver;
        public Color BackColor { get; set; } = Color.White;
        public Font Font { get; set; } = new Font("arial", 24);
        public Brush ForeColor { get; set; } = Brushes.Gray;
    }

}
