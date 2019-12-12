using System.Threading.Tasks;
using Roomies2.WebApp.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Model.People.OAuth;
using System;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Roomies2.WebApp.Authentication
{
    public class GoogleAuthenticationManager : AuthenticationManager<GoogleUserInfo>
    {
        public UserService UserService { get; }
        public UserGateway Gateway { get; }

        public GoogleAuthenticationManager(UserService userService, UserGateway userGateway)
        {
            UserService = userService;
            Gateway = userGateway;
        }

        protected override async Task CreateOrUpdateUser(GoogleUserInfo userInfo)
        {
            if (userInfo.RefreshToken != null)
            {
                await Gateway.CreateOrUpdateGoogleUser(userInfo.Email, userInfo.GoogleId, userInfo.RefreshToken); 
            }
        }

        protected override Task<UserData> FindUser(GoogleUserInfo userInfo)
        {
            return Gateway.FindByGoogleId(userInfo.GoogleId);
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
                GoogleId = ctx.GetGoogleId(),
                
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
