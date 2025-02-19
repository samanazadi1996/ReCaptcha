using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace Sam.ReCaptcha.Extensions;

internal static class KeyGeneratorExtensions
{
    public static string Generate(Guid id, ReCaptchaOptions options, HttpContext? context)
    {
        // Estimate total string length to minimize allocations
        var estimatedLength = 20 + (options.UseIpAddressBinding ? 15 : 0); // "ReCaptcha_" + IP + Guid
        var keyBuilder = new StringBuilder(estimatedLength);

        keyBuilder.Append("ReCaptcha");

        if (options.UseIpAddressBinding)
        {
            if (context is not null)
            {
                var ip = context.GetIpAddress();
                Console.WriteLine($"Using IP Address \"{ip}\" in Captcha Key");

                keyBuilder.Append('_').Append(ip);
            }
            else
            {
                // Log a warning instead of using an invalid key
                Console.WriteLine("Warning: IP binding is enabled, but HttpContext was not provided.");
            }
        }

        // Append Guid without dashes for optimization
        keyBuilder.Append('_').Append(id.ToString("N")); // "N" -> Compact format without dashes

        return keyBuilder.ToString();
    }
}