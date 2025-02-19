using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace Sam.ReCaptcha.Extensions;

internal static class KeyGeneratorExtensions
{
    public static string Generate(Guid id, ReCaptchaOptions options, HttpContext context)
    {
        // تخمین طول کل رشته برای جلوگیری از تخصیص‌های اضافی
        var estimatedLength = 20 + (options.UseIpAddressBinding? 15 : 0); // "ReCaptcha_" + IP + Guid
        var keyBuilder = new StringBuilder(estimatedLength);

        keyBuilder.Append("ReCaptcha");

        if (options.UseIpAddressBinding)
        {
            var ip = context.GetIpAddress();
            Console.WriteLine($"Using IP Address \"{ip}\" in Captcha Key");

            keyBuilder.Append('_').Append(ip);
        }

        // Guid را بدون قالب‌بندی اضافی اضافه می‌کنیم
        keyBuilder.Append('_').Append(id.ToString("N")); // "N" -> بدون خط تیره برای بهینه‌سازی

        return keyBuilder.ToString();
    }
}