using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Sam.ReCaptcha.Services;

/// <summary>
/// Defines methods for generating and validating CAPTCHA images.
/// </summary>
public interface IReCaptchaService
{
    /// <summary>
    /// Generates a CAPTCHA image based on a unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier for the CAPTCHA.</param>
    /// <param name="context">The optional HTTP context to bind the CAPTCHA to an IP address.</param>
    /// <returns>A byte array representing the generated CAPTCHA image.</returns>
    Task<byte[]> GenerateCaptchaImageAsync(Guid id, HttpContext? context = null);

    /// <summary>
    /// Validates the provided CAPTCHA code against the stored value.
    /// </summary>
    /// <param name="id">The unique identifier for the CAPTCHA.</param>
    /// <param name="code">The user-provided CAPTCHA code.</param>
    /// <param name="context">The optional HTTP context to validate IP-bound CAPTCHA instances.</param>
    /// <returns>True if the CAPTCHA code is valid; otherwise, false.</returns>
    Task<bool> Validate(Guid id, string code, HttpContext? context = null);
}