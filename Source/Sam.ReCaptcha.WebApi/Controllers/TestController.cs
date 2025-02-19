using Microsoft.AspNetCore.Mvc;
using Sam.ReCaptcha.Services;

namespace Sam.ReCaptcha.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TestController(IReCaptchaService captchaService) : ControllerBase
{

    [HttpGet]
    public async Task<bool> Validate([FromQuery] Guid id, [FromQuery] string code)
    {
        return await captchaService.Validate(id, code, HttpContext);
    }
}