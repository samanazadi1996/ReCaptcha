using Microsoft.AspNetCore.Mvc;
using Sam.ReCaptcha.Services;

namespace Sam.ReCaptcha.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CaptchaController(ICaptchaService captchaService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<FileContentResult> ReCaptcha(Guid id)
    {
        var bytes = await captchaService.GenerateCaptchaImageAsync(id);

        return File(bytes, "image/jpeg");
    }

    [HttpGet("{id:guid}")]
    public async Task<bool> Validate(Guid id, [FromQuery] string code)
    {
        return await captchaService.Validate(id, code);
    }
}