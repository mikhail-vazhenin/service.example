using System.Net;
using System.Net.Http;

namespace Service.Example.YaAudience.Tools.Handlers
{
    public class CookieHandler : HttpClientHandler
    {
        public CookieHandler(CookieContainer cookieContainer)
        {
            CookieContainer = cookieContainer;
            UseCookies = true;
            UseDefaultCredentials = true;
        }
    }
}
