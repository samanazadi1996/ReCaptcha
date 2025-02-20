using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();

var captchaOptions = new CaptchaOptions
{
    CaptchaVariant = CaptchaTypes.Default,
    AllowedCharacters = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789",
    CodeLength = 6,
    Font = ReCaptchaFonts.DejaVuSansBold,
    UseIpAddressBinding = true,
    ExpirationTimeInMinutes = 5,
    CaseSensitivityMode = StringComparison.OrdinalIgnoreCase,

    TextOptions = new CaptchaTextOptions
    {
        MinFontSize = 30,
        MaxFontSize = 36,
        TextColor = null,
        ShadowColor = Color.Gray,
        LetterSpacing = 30,
        ShadowPositionOffset = new PointF(2, 2),
        Rotation = new RotationOptions
        {
            MinRotation = -10,
            MaxRotation = 10
        }
    },

    LineDistortion = new LineDistortionOptions
    {
        LineColor = null,
        LineCount = 8
    },

    NoiseEffect = new NoiseEffectOptions
    {
        NoiseDensity = 200,
        NoiseColor = null
    },

    GradientBackground = new GradientBackgroundOptions
    {
        GradientStops = new[]
        {
            new ColorStop(0, Color.LightBlue),
            new ColorStop(0.5f, Color.White),
            new ColorStop(1, Color.LightGray)
        }
    }
};

builder.Services.AddCaptcha(captchaOptions);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();