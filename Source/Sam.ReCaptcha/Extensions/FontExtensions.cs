﻿using Microsoft.Extensions.DependencyInjection;
using SixLabors.Fonts;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Sam.ReCaptcha.Extensions;

internal static class FontExtensions
{
    public static FontFamily? FontFamily;

    public static FontFamily GetFont(ReCaptchaOptions captchaOptions)
    {
        if (FontFamily != null)
            return FontFamily.Value;

        var fontNames = new Dictionary<ReCaptchaFonts, string>()
        {
            { ReCaptchaFonts.DejaVuSansBold, "Sam.ReCaptcha.Assets.DejaVuSans-Bold.ttf" } // Namespace + filename
        };

        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = fontNames[captchaOptions.Font];

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            throw new FileNotFoundException($"Embedded resource {resourceName} not found.");

        var fontCollection = new FontCollection();
        FontFamily = fontCollection.Add(stream);

        return FontFamily.Value;
    }
}