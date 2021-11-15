using Sam.ReCaptcha.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sam.ReCaptcha.Persistent
{
    internal class PersistentInMemory : IPersistentInMemory
    {
        public List<Token> Tokens = new List<Token>();
        public async Task Add(string key, string value, string ip)
        {
            await RemoveExpiredValue();

            var token = Tokens.FirstOrDefault(p => p.Key.Equals(key));

            if (token is null)
            {
                Tokens.Add(new Token()
                {
                    Key = key,
                    Value = value,
                    InsertDateTime = DateTime.Now,
                    Ip = ip
                });
            }
            else
            {
                token.Key = key;
                token.Value = value;
                token.InsertDateTime = DateTime.Now;
                token.Ip = ip;
            }
        }

        public async Task<string> Get(string key, string ip)
        {
            await RemoveExpiredValue();

            var token = Tokens.FirstOrDefault(p => p.Key.Equals(key) && p.Ip.Equals(ip));

            if (token is null) return null;
            
            Tokens.Remove(token);
            
            return token.Value;
        }
        private Task RemoveExpiredValue()
        {
            return Task.Run(() =>
            {
                var tenMinAgo = DateTime.Now.AddMinutes(-10);

                Tokens = Tokens.Where(p => p.InsertDateTime > tenMinAgo).ToList();
            });
        }
    }
}
