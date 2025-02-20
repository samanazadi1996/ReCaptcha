using Microsoft.AspNetCore.Mvc;
using Sam.ReCaptcha.Services;
using System;
using System.Threading.Tasks;

namespace Sam.ReCaptcha.Controllers;

[ApiController]
[Route("Api/ReCaptcha")]
public class CaptchaController(ICaptchaService captchaService) : ControllerBase
{

    [HttpGet("{id:guid}")]
    public async Task<FileContentResult> ReCaptcha(Guid id)
    {

        var bytes = await captchaService.GenerateCaptchaImageAsync(id, HttpContext);

        return File(bytes, "image/jpeg");
    }
}
