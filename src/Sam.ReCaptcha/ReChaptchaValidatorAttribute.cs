using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Sam.ReCaptcha.Extensions;
using Sam.ReCaptcha.Persistent;
using System;
using System.Threading.Tasks;

namespace Sam.ReCaptcha
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ReChaptchaValidatorAttribute : ActionFilterAttribute
    {
        private string InputName { get; set; }
        private string ErrorMessage { get; set; }
        public ReChaptchaValidatorAttribute(string inputName, string errorMessage = null)
        {
            InputName = inputName;
            ErrorMessage = errorMessage ?? "Chaptcha is not Valid";
        }
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var ReCapthchaSessionId = context.HttpContext.Request.Form["ReCapthchaId"].ToString();

            var persistentInMemory = context.HttpContext.RequestServices.GetRequiredService<IPersistentInMemory>();

            var data = persistentInMemory.Get($"ReCaptcha-{ReCapthchaSessionId}", context.HttpContext.GetIpAddress()).Result;

            var input = context.HttpContext.Request.Form[InputName];

            if (data is null || !data.Equals(input)) context.ModelState.AddModelError(InputName, ErrorMessage);

            return base.OnActionExecutionAsync(context, next);
        }
    }

}
