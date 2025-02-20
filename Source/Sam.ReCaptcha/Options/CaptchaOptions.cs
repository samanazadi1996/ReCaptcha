using SixLabors.ImageSharp.Drawing.Processing;
using System;
using SixLabors.ImageSharp;

namespace Microsoft.Extensions.DependencyInjection;

public class CaptchaOptions
{
    public CaptchaTypes CaptchaVariant { get; set; } = CaptchaTypes.Default;
    public int? Width { get; set; }
    public int Height { get; set; } = 80;
    public string AllowedCharacters { get; set; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public CaptchaTextOptions TextOptions { get; set; } = new();
    public int CodeLength { get; set; } = 5;
    public ReCaptchaFonts Font { get; set; } = ReCaptchaFonts.DejaVuSansBold;
    public bool UseIpAddressBinding { get; set; } = false;
    public LineDistortionOptions? LineDistortion { get; set; } = new();
    public long ExpirationTimeInMinutes { get; set; } = 10;
    public GradientBackgroundOptions? GradientBackground { get; set; }
    public NoiseEffectOptions? NoiseEffect { get; set; }
    public StringComparison CaseSensitivityMode { get; set; } = StringComparison.OrdinalIgnoreCase;
}
public class CaptchaTextOptions
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
    DejaVuSansBold
}
public enum CaptchaTypes
{
    Default
}