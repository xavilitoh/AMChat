using Colma2.API.Models;
using Colma2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMChat.Helpers
{
    public interface IFacebookAuthService
    {
        Task<FacebookTokenValidationResult> ValidateAccessAsync(string AccessToken);

        Task<FacebokUserInfo> GetUserInfoAsync(string AccessToken);
    }
}
