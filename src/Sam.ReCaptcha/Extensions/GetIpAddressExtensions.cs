using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Sam.ReCaptcha.Extensions
{
    public static class GetIpAddressExtensions
    {
        public static string GetIpAddress(this HttpContext httpContext)
        {
            string IpAddress;
            if (!string.IsNullOrEmpty(httpContext.Request.Headers["X-Forwarded-For"]))
                IpAddress = httpContext.Request.Headers["X-Forwarded-For"];
            else
                IpAddress = httpContext.Request.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();

            return IpAddress;
        }

    }
}
