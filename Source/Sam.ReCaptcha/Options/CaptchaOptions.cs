using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using System;

namespace Microsoft.Extensions.DependencyInjection;

public class CaptchaOptions
{
    public MathCaptchaOptions? MathCaptchaOptions { get; set; } = new();
    public TextCaptchaOptions? TextCaptchaOptions { get; set; } = new();
    public CaptchaTypes CaptchaVariant { get; set; } = CaptchaTypes.DefaultCaptcha;
    public int? Width { get; set; }
    public int Height { get; set; } = 80;
    public CaptchaImageOptions ImageOptions { get; set; } = new();
    public ReCaptchaFonts Font { get; set; } = ReCaptchaFonts.DejaVuSansBold;
    public LineDistortionOptions? LineDistortion { get; set; } = new();
    public long ExpirationTimeInMinutes { get; set; } = 10;
    public GradientBackgroundOptions? GradientBackground { get; set; }
    public NoiseEffectOptions? NoiseEffect { get; set; }
    public StringComparison CaseSensitivityMode { get; set; } = StringComparison.OrdinalIgnoreCase;

    public CaptchaOptions UseDefaultCaptcha(Action<TextCaptchaOptions> configureOptions)
    {
        CaptchaVariant = CaptchaTypes.DefaultCaptcha;
        var options = new TextCaptchaOptions();
        configureOptions(options);
        TextCaptchaOptions = options;
        return this;
    }
    public CaptchaOptions UseDefaultCaptcha()
    {
        CaptchaVariant = CaptchaTypes.DefaultCaptcha;
        TextCaptchaOptions = new TextCaptchaOptions();
        return this;
    }
    public CaptchaOptions UseMathCaptcha(Action<MathCaptchaOptions> configureOptions)
    {
        CaptchaVariant = CaptchaTypes.MathCaptcha;
        var options = new MathCaptchaOptions();
        configureOptions(options);
        MathCaptchaOptions = options;
        return this;
    }
    public CaptchaOptions UseMathCaptcha()
    {
        CaptchaVariant = CaptchaTypes.MathCaptcha;
        MathCaptchaOptions = new MathCaptchaOptions();
        return this;
    }
}
public class MathCaptchaOptions
{
    public int MinValue { get; set; } = 10;
    public int MaxValue { get; set; } = 99;
}
public class TextCaptchaOptions
{
    public string AllowedCharacters { get; set; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public int CodeLength { get; set; } = 5;

}
public class CaptchaImageOptions
{

    public int MinFontSize { get; set; } = 28;
    public int MaxFontSize { get; set; } = 34;
    public Color? TextColor { get; set; }
    public Color? ShadowColor { get; set; } = Color.Gray;
    public int LetterSpacing { get; set; } = 30;
    public RotationOptions? Rotation { get; set; } = new();
    public PointF ShadowPositionOffset { get; set; } = new(1, 1);
    public int StartXPosition { get; set; } = 20;

}
public class RotationOptions
{
    public int MinRotation { get; set; } = -15;
    public int MaxRotation { get; set; } = 15;
}
public class LineDistortionOptions
{
    public Color? LineColor { get; set; }
    public int LineCount { get; set; } = 10;
}

public class NoiseEffectOptions
{
    public int NoiseDensity { get; set; } = 300;
    public Color? NoiseColor { get; set; }
}

public class GradientBackgroundOptions
{
    public ColorStop[] GradientStops { get; set; } = [new ColorStop(0, Color.LightSkyBlue), new ColorStop(1, Color.White)];
}

public enum ReCaptchaFonts
{
    DejaVuSansBold,
    Hevilla,
    Timetwist
}
public enum CaptchaTypes
{
    DefaultCaptcha,
    MathCaptcha

}