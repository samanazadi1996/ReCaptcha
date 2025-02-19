using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Sam.ReCaptcha.Services;

public interface IReCaptchaService
{
    Task<byte[]> GenerateCaptchaImageAsync(Guid id, HttpContext context);
}