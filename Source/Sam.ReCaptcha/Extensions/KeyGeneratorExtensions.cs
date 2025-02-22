using System;

namespace Sam.ReCaptcha.Extensions;

internal static class KeyGeneratorExtensions
{
    public static string Generate(Guid id)
        => $"{nameof(ReCaptcha)}_{id}";
}