using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Sam.ReCaptcha.Extensions;
using System;
using System.Threading.Tasks;

namespace Sam.ReCaptcha.Services.Implementations;

internal class MathCaptchaService(SharedServices sharedServices, CaptchaOptions captchaOptions, IDistributedCache distributedCache) : ICaptchaService
{
    public async Task<byte[]> GenerateCaptchaImageAsync(Guid id)
    {
        var (code, math) = CodeGeneratorExtensions.GenerateMath(captchaOptions);
        var key = KeyGeneratorExtensions.Generate(id);

        await distributedCache.SetStringAsync(key, code, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(captchaOptions.ExpirationTimeInMinutes)
        });

        return sharedServices.RenderCaptchaImage(math);
    }

    public async Task<bool> Validate(Guid id, string code)
    {
        var key = KeyGeneratorExtensions.Generate(id);
        try
        {
            if (!int.TryParse(code, out var numCode))
                return false;

            var captchaText = await distributedCache.GetStringAsync(key);

            return Convert.ToInt32(captchaText).Equals(numCode);
        }
        finally
        {
            if (!string.IsNullOrEmpty(key))
                await distributedCache.RemoveAsync(key);
        }
    }

}