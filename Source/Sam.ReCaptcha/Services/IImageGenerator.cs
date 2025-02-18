using System;
using System.Collections.Generic;
using System.Text;

namespace Sam.ReCaptcha.Services;

public interface IImageGenerator
{
    byte[] GetBytes(string data);
}
