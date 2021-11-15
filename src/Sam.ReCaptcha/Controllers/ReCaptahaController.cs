using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sam.ReCaptcha.Extensions;
using Sam.ReCaptcha.Persistent;
using Sam.ReCaptcha.Services;
using System.Threading.Tasks;

namespace Sam.ReCaptcha.Controllers
{
    public class ReCaptahaController : Controller
    {
        private readonly ICodeGenerator _codeGenerator;
        private readonly IImageGenerator _imageGenerator;
        private readonly IPersistentInMemory _persistentInMemory;

        public ReCaptahaController(ICodeGenerator codeGenerator, IImageGenerator imageGenerator, IPersistentInMemory persistentInMemory)
        {
            _codeGenerator = codeGenerator;
            _imageGenerator = imageGenerator;
            _persistentInMemory = persistentInMemory;
        }

        [HttpGet("ReCaptcha/{id}")]
        public async Task<FileContentResult> ReCaptcha(string id)
        {
            string data = _codeGenerator.Generate();

            await _persistentInMemory.Add($"ReCaptcha-{id}", data, HttpContext.GetIpAddress());

            var bytes = _imageGenerator.GetBytes(data);
            
            return File(bytes, "image/jpeg");
        }
    }
}
