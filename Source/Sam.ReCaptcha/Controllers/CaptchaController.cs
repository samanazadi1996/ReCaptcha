using Microsoft.AspNetCore.Mvc;
using Sam.ReCaptcha.Services;
using System;
using System.Threading.Tasks;

namespace Sam.ReCaptcha.Controllers;

public class ReCaptchaController(IReCaptchaService captchaService) : Controller
{

    [HttpGet("ReCaptcha/{id:guid}")]
    public async Task<FileContentResult> ReCaptcha(Guid id)
    {

        var bytes = await captchaService.GenerateCaptchaImageAsync(id, HttpContext);

        return File(bytes, "image/jpeg");
    }
}
