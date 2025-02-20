﻿using System;
using System.Linq;

namespace Sam.ReCaptcha.Services
{
    internal class CodeGenerator : ICodeGenerator
    {
        private readonly ReCaptchaOptions reCaptchaOptions;

        public CodeGenerator(ReCaptchaOptions reCaptchaOptions)
        {
            this.reCaptchaOptions = reCaptchaOptions;
        }

        public string Generate()
        {
            var random = new Random();
            var str = reCaptchaOptions.CodeCharacter;
            return new string(Enumerable.Repeat(str, 4).Select(s => s[random.Next(str.Length)]).ToArray());
        }
    }
    public interface ICodeGenerator
    {
        string Generate();
    }
}
