using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Sam.ReCaptcha.Extensions;

internal static class CodeGeneratorExtensions
{
    public static string Generate(ReCaptchaOptions captchaOptions)
    {
        var random = new Random();
        var str = captchaOptions.AllowedCharacters;

        return new string(Enumerable.Repeat(str, captchaOptions.CodeLength)
            .Select(s => s[random.Next(str.Length)]).ToArray());
    }
}