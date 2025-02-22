using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Sam.ReCaptcha.Extensions;
using System;
using System.Threading.Tasks;

namespace Sam.ReCaptcha.Services.Implementations;

internal class DefaultCaptchaService(SharedServices sharedServices, CaptchaOptions captchaOptions, IDistributedCache distributedCache) : ICaptchaService
{
    public async Task<byte[]> GenerateCaptchaImageAsync(Guid id)
    {
        var code = CodeGeneratorExtensions.GenerateCode(captchaOptions);
        var key = KeyGeneratorExtensions.Generate(id);

        await distributedCache.SetStringAsync(key, code, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(captchaOptions.ExpirationTimeInMinutes)
        });

        return sharedServices.RenderCaptchaImage(code);
    }

    public async Task<bool> Validate(Guid id, string code)
    {
        var key = KeyGeneratorExtensions.Generate(id);
        try
        {
            if (code.Length != captchaOptions.TextCaptchaOptions!.CodeLength)
                return false;

            var captchaText = await distributedCache.GetStringAsync(key);

            return !string.IsNullOrEmpty(captchaText) && captchaText.Equals(code, captchaOptions.TextCaptchaOptions.CaseSensitivityMode);
        }
        finally
        {
            if (!string.IsNullOrEmpty(key))
                await distributedCache.RemoveAsync(key);
        }
    }

}