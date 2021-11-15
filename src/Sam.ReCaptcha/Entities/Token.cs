using System;

namespace Sam.ReCaptcha.Entities
{
    internal class Token
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime InsertDateTime { get; set; }
        public string Ip { get; set; }
    }
}
