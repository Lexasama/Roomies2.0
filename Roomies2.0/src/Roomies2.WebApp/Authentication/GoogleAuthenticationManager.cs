using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.WebApp.Services;

namespace Roomies2.WebApp.Authentication
{
    public class GoogleAuthenticationManager : AuthenticationManager<GoogleUserInfo>
    {
        UserGateway _userGateway;
        UserService _userService;
        public GoogleAuthenticationManager(UserService userService, UserGateway userGateway)
        {
            _userService = userService;
            _userGateway = userGateway;
        }

        protected override async Task CreateOrUpdateUser(GoogleUserInfo userInfo)
        {
            if (userInfo.RefreshToken != null)
            {
                await _userGateway.CreateOrUpdateGoogleUser(userInfo.Email, userInfo.GoogleId, userInfo.RefreshToken); 
            }
        }

        protected override Task<UserData> FindUser(GoogleUserInfo userInfo)
        {
            return _userGateway.FindByGoogleId(userInfo.GoogleId);
        }

        protected override Task<GoogleUserInfo> GetUserInfoFromContext(OAuthCreatingTicketContext ctx)
        {
           //using( HttpClient httpClient = new HttpClient())
           //{
           //     httpClient.GetAsync("https://www.googleapis.com/plus/v1/people/me?personfilels=Birthdates");
           //};
           
           
            return Task.FromResult(new GoogleUserInfo
            {
                RefreshToken = ctx.RefreshToken,
                Email = ctx.GetEmail(),
                GoogleId = ctx.GetGoogleId()
            });
        }
    }


    public class GoogleUserInfo
    {
        public string RefreshToken { get; set; }

        public string Email { get; set; }

        public string GoogleId { get; set; }
    }
}