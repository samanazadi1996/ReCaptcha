using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Sam.ReCaptcha.Extensions;

internal static class IpAddressExtensions
{
    public static string GetIpAddress(this HttpContext httpContext)
    {
        string ipAddress;
        if (!string.IsNullOrEmpty(httpContext.Request.Headers["X-Forwarded-For"]))
            ipAddress = httpContext.Request.Headers["X-Forwarded-For"];
        else
            ipAddress = httpContext.Request.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();

        return ipAddress;
    }

}