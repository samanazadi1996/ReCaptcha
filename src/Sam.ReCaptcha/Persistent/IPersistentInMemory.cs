using System.Threading.Tasks;

namespace Sam.ReCaptcha.Persistent
{
    public interface IPersistentInMemory
    {
        Task Add(string key, string value, string ip);
        Task<string> Get(string key, string ip);
    }
}
