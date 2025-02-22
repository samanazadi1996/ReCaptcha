using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Sam.ReCaptcha.Extensions;

internal static class CodeGeneratorExtensions
{
    public static Random Random = new();
    public static string GenerateCode(CaptchaOptions captchaOptions)
    {
        var options = captchaOptions.TextCaptchaOptions!;

        var str = options.AllowedCharacters;

        return new string(Enumerable.Repeat(str, options.CodeLength)
            .Select(s => s[Random.Next(str.Length)]).ToArray());
    }

    public static (string, string) GenerateMath(CaptchaOptions captchaOptions)
    {
        var options = captchaOptions.MathCaptchaOptions!;

        var randomNumber = Random.Next(options.MinValue, options.MaxValue);

        return (randomNumber.ToString(), GenerateEquation(randomNumber));

        string GenerateEquation(int number)
        {
            var isAddition = Random.Next(2) == 0;

            int a, b;
            if (isAddition)
            {
                a = Random.Next(options.MinValue, number);
                b = number - a;
                return $"{a}+{b}";
            }

            a = Random.Next(number, options.MaxValue + Random.Next(options.MinValue, options.MaxValue));
            b = a - number;
            return $"{a}-{b}";
        }
    }
}