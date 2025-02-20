using Microsoft.AspNetCore.Http;
using Sam.ReCaptcha.Services;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sam.ReCaptcha.Controllers;

[ApiController]
[Route("Api/ReCaptcha")]
public class ReCaptchaController(IReCaptchaService captchaService) : ControllerBase
{

    [HttpGet("{id:guid}")]
    public async Task<FileContentResult> ReCaptcha(Guid id)
    {

        var bytes = await captchaService.GenerateCaptchaImageAsync(id, HttpContext);

        return File(bytes, "image/jpeg");
    }
}
