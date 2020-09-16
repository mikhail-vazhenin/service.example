using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Service.Example.YaAudience.Configuration;
using Service.Example.YaAudience.Interfaces.Clients;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Service.Example.YaAudience.Services.Statistics
{

    /// <summary>
    /// Аутентификатор через passport.yandex.ru
    /// </summary>
    /// <remarks>
    /// Используется для получения данных с формы audience
    /// </remarks>
    public class PassportAuthenticator
    {
        private const string CsrfTokenName = "csrf_token";

        private readonly string _login;
        private readonly string _pass;

        private readonly IYandexPassportClient _passportClient;
        private readonly CookieContainer _cookieContainer;

        public PassportAuthenticator(IYandexPassportClient passportClient,
            CookieContainer cookieContainer,
            string login,
            string pass)
        {
            _passportClient = passportClient;
            _cookieContainer = cookieContainer;
            _login = login;
            _pass = pass;
        }

        private async Task<bool> IsAuthenticated(string csrf)
        {
            var response = await _passportClient.GetProfilePageResponse().ConfigureAwait(false);

            return !response.RequestMessage.RequestUri.AbsolutePath.Contains("auth", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Получение авторизационных Cookie
        /// </summary>
        /// <remarks>
        /// Получение происходит в несколько этапов:
        /// 1. Открытие главной страницы (получаем csrf и внутренние cookie)            [/profile]
        /// 2. Отправляем запрос с логином + csrf + cookie (из ответа берем trackId)    [/registration-validations/auth/multi_step/start]
        /// 3. Отправляем запрос с паролем + trackId (из него получаем cookie)          [/registration-validations/auth/multi_step/commit_password]
        /// </remarks>
        /// <param name="v"></param>
        /// <returns></returns>
        public async Task Authenticate()
        {
            var csrfCode = await GetCsrfCode().ConfigureAwait(false);

            if (!await IsAuthenticated(csrfCode))
            {
                var trackId = await GetTrackId(csrfCode).ConfigureAwait(false);
                await SaveAuthCookie(csrfCode, trackId).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 1. Открытие главной страницы (получаем csrf и внутренние cookie)            [/profile]
        /// </summary>
        private async Task<string> GetCsrfCode()
        {
            var mainPageContent = await _passportClient.GetProfilePage();

            var html = new HtmlDocument();
            html.LoadHtml(mainPageContent);

            var csrf = html.DocumentNode.SelectSingleNode("//html//body").Attributes["data-csrf"].Value;
            return csrf;
        }

        /// <summary>
        /// 2. Отправляем запрос с логином + csrf + cookie (из ответа берем trackId)    [/registration-validations/auth/multi_step/start]
        /// </summary>
        private async Task<string> GetTrackId(string csrf)
        {
            var parameters = new Dictionary<string, object>
            {
                [CsrfTokenName] = csrf,
                ["login"] = _login,
                ["process_uuid"] = Guid.NewGuid().ToString("N"),
            };

            var loginPageContent = await _passportClient.SendAuthLogin(parameters);
            var trackId = loginPageContent["track_id"].Value<string>();

            return trackId;
        }

        /// <summary>
        /// 3. Отправляем запрос с паролем + trackId (из него получаем cookie)          [/registration-validations/auth/multi_step/commit_password]
        /// </summary>
        /// <param name="csrf"></param>
        /// <param name="trackId"></param>
        /// <returns></returns>
        private async Task SaveAuthCookie(string csrf, string trackId)
        {

            var parameters = new Dictionary<string, object>
            {
                [CsrfTokenName] = csrf,
                ["password"] = _pass,
                ["track_id"] = trackId,

            };
            await _passportClient.SendAuthPass(parameters);
        }


        private void AddAcceptHeaders(HttpRequestHeaders headers)
        {
            headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            //headers.Add("Accept-Encoding", "gzip,deflate,br");
            headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7,fr;q=0.6,de;q=0.5,da;q=0.4");
        }

    }
}
