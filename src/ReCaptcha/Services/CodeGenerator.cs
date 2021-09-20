using System;
using System.Linq;

namespace ReCaptcha.Services
{
    internal class CodeGenerator : ICodeGenerator
    {
        public string Generate()
        {
            var random = new Random();
            var str = "0123456789";
            return new string(Enumerable.Repeat(str, 4).Select(s => s[random.Next(str.Length)]).ToArray());
        }
    }
    public interface ICodeGenerator
    {
        string Generate();
    }
}
