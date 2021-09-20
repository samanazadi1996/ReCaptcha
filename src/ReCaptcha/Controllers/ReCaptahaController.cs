using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReCaptcha.Services;

namespace ReCaptcha.Controllers
{
    public class ReCaptahaController : Controller
    {
        private readonly ICodeGenerator _codeGenerator;
        private readonly IImageGenerator _imageGenerator;

        public ReCaptahaController(ICodeGenerator codeGenerator, IImageGenerator imageGenerator)
        {
            _codeGenerator = codeGenerator;
            _imageGenerator = imageGenerator;
        }

        [HttpGet("ReCaptcha/{id}")]
        public FileContentResult ReCaptcha(string id)
        {
            string data = _codeGenerator.Generate();
            HttpContext.Session.SetString("ReCaptcha" + id, data);
            var bytes = _imageGenerator.GetBytes(data);
            return File(bytes, "image/jpeg");
        }
    }
}
