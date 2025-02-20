using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Sam.ReCaptcha.Extensions;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Sam.ReCaptcha.Services.Implementations;

internal class ReCaptchaService(ReCaptchaOptions captchaOptions, IDistributedCache distributedCache) : IReCaptchaService
{
    public async Task<byte[]> GenerateCaptchaImageAsync(Guid id, HttpContext? context = null)
    {
        var code = CodeGeneratorExtensions.Generate(captchaOptions);
        var key = KeyGeneratorExtensions.Generate(id, captchaOptions, context);

        await distributedCache.SetStringAsync(key, code, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(captchaOptions.ExpirationTimeInMinutes)
        });

        return RenderCaptchaImage(code);
    }

    public async Task<bool> Validate(Guid id, string code, HttpContext? context = null)
    {
        var key = KeyGeneratorExtensions.Generate(id, captchaOptions, context);
        try
        {
            if (code.Length != captchaOptions.CodeLength)
                return false;

            var captchaText = await distributedCache.GetStringAsync(key);

            return !string.IsNullOrEmpty(captchaText) && captchaText.Equals(code, captchaOptions.CaseSensitivityMode);
        }
        finally
        {
            if (!string.IsNullOrEmpty(key))
                await distributedCache.RemoveAsync(key);
        }
    }

    private byte[] RenderCaptchaImage(string captchaText)
    {
        var width = captchaOptions.Width ?? captchaText.Length * 30 + 90;
        var height = captchaOptions.Height;
        var random = new Random();

        using var image = new Image<Rgba32>(width, height);

        if (captchaOptions.GradientBackground is not null)
        {
            image.Mutate(ctx => ctx.Fill(
                new LinearGradientBrush(
                    new PointF(0, 0),
                    new PointF(width, height),
                    GradientRepetitionMode.None,
                    captchaOptions.GradientBackground.GradientStops
                )));
        }

        if (captchaOptions.NoiseEffect is not null)
        {
            for (var i = 0; i < captchaOptions.NoiseEffect.NoiseDensity; i++)
            {
                var noiseColor = captchaOptions.NoiseEffect.NoiseColor ??
                                 new Rgba32((byte)random.Next(200, 255), (byte)random.Next(200, 255), (byte)random.Next(200, 255), 150);
                var point = new Point(random.Next(0, width), random.Next(0, height));

                image.Mutate(ctx => ctx.DrawLine(noiseColor, 0.8f, new PointF[]
                {
                    point,
                    new(point.X + random.Next(-5, 5), point.Y + random.Next(-5, 5))
                }));
            }
        }

        if (captchaOptions.LineDistortion is not null && captchaOptions.LineDistortion.LineCount > 0)
        {
            for (var i = 0; i < captchaOptions.LineDistortion.LineCount; i++)
            {
                var bezierColor = captchaOptions.LineDistortion.LineColor ??
                                  new Rgba32((byte)random.Next(0, 100), (byte)random.Next(0, 100), (byte)random.Next(0, 100), 128);

                image.Mutate(ctx => ctx.DrawBeziers(
                    bezierColor,
                    2f,
                    new PointF(random.Next(0, width), random.Next(0, height)),
                    new PointF(random.Next(0, width), random.Next(0, height)),
                    new PointF(random.Next(0, width), random.Next(0, height)),
                    new PointF(random.Next(0, width), random.Next(0, height))
                ));
            }
        }

        var textOptions = captchaOptions.TextOptions;

        float xPos = 20;
        foreach (var character in captchaText)
        {
            var textColor = textOptions.TextColor ?? new Rgba32((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));

            using var letterImage = new Image<Rgba32>(65, 55);
            letterImage.Mutate(ctx => ctx.Fill(Color.Transparent));

            var fontSize = random.Next(textOptions.MinFontSize, textOptions.MaxFontSize);
            var font = new Font(FontExtensions.GetFont(captchaOptions), fontSize);

            if (textOptions.ShadowColor is not null)
            {
                letterImage.Mutate(ctx => ctx.DrawText(
                    new DrawingOptions(),
                    character.ToString(),
                    font,
                    textOptions.ShadowColor.Value,
                    new PointF(25 + textOptions.ShadowPositionOffset.X, 25 + textOptions.ShadowPositionOffset.Y)
                ));
            }

            letterImage.Mutate(ctx => ctx.DrawText(
                new DrawingOptions(),
                character.ToString(),
                font,
                textColor,
                new PointF(25, 25)
            ));

            if (textOptions.Rotation is not null)
            {
                float rotation = random.Next(textOptions.Rotation.MinRotation, textOptions.Rotation.MaxRotation);
                letterImage.Mutate(ctx => ctx.Rotate(rotation));
            }


            image.Mutate(ctx => ctx.DrawImage(letterImage, new Point((int)xPos, random.Next(-10, 10)), 1));

            xPos += textOptions.LetterSpacing;
        }

        using var ms = new MemoryStream();
        image.SaveAsPng(ms);
        return ms.ToArray();
    }
}
