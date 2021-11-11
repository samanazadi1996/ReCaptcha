using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Sam.ReCaptcha
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ReChaptchaValidatorAttribute : ActionFilterAttribute
    {
        private string inputName { get; set; }
        private string errorMessage { get; set; }
        public ReChaptchaValidatorAttribute(string inputName, string errorMessage = null)
        {
            this.inputName = inputName;
            this.errorMessage = errorMessage ?? "Chaptcha is not Valid";
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ReCapthchaSessionId = context.HttpContext.Request.Form["ReCapthchaSessionId"].ToString();

            var data = context.HttpContext.Session.GetString("ReCaptcha" + ReCapthchaSessionId);
            var input = context.HttpContext.Request.Form[inputName];

            if (data is null || !data.Equals(input)) context.ModelState.AddModelError(inputName, errorMessage);

            context.HttpContext.Session.Remove("ReCaptcha" + ReCapthchaSessionId);

        }
    }

}
