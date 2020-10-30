using AMChat.API.Models;
using AMChat.ViewModels;
using Colma2.API.Models;
using Colma2.Helpers;
using Colma2.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AMChat.Helpers
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private const string TokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string UserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email,birthday&access_token={0}";
        private readonly FacebookAuthSettings _facebookAuthSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public FacebookAuthService(FacebookAuthSettings facebookAuthSettings, IHttpClientFactory httpClientFactory)
        {
            _facebookAuthSettings = facebookAuthSettings;
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<FacebokUserInfo> GetUserInfoAsync(string AccessToken)
        {
            
            var FormattedUrl = String.Format(UserInfoUrl, AccessToken);
            HttpResponseMessage result = await _httpClientFactory.CreateClient().GetAsync(FormattedUrl);
            result.EnsureSuccessStatusCode();

            var responseAsString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebokUserInfo>(responseAsString);
        }

        public async Task<FacebookTokenValidationResult> ValidateAccessAsync(string AccessToken)
        {            
            var FormattedUrl = String.Format(TokenValidationUrl, AccessToken, _facebookAuthSettings.AppId, _facebookAuthSettings.AppSecret);

            HttpResponseMessage result = await _httpClientFactory.CreateClient().GetAsync(FormattedUrl);
            result.EnsureSuccessStatusCode();

            var responseAsString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);
        }
    }
}
