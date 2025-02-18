using Microsoft.Extensions.DependencyInjection;

namespace Sam.ReCaptcha.Services.implementations;

internal class ImageGenerator(ReCaptchaOptions reCaptchaOptions) : IImageGenerator
{
    public byte[] GetBytes(string data)
    {
        return new byte[data.Length]; ;
    }
}
