using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Model.People.OAuth;
using Roomies2.WebApp.Services;

namespace Roomies2.WebApp.Authentication
{
    public class FacebookAuthenticationManager : AuthenticationManager<FacebookUserInfo>
    {
        public FacebookAuthenticationManager(UserService userService, UserGateway userGateway)
        {
            UserService = userService;
            UserGateway = userGateway;
        }

        protected override async Task CreateOrUpdateUser(FacebookUserInfo userInfo)
        {
            if (userInfo.RefreshToken != null)
            {
                await UserGateway.CreateOrUpdateFacebookUser( userInfo.Email, userInfo.FacebookId, userInfo.RefreshToken);
            }
        }


        protected override Task<UserData> FindUser(FacebookUserInfo userInfo)
        {
            return UserGateway.FindByFacebookId(userInfo.FacebookId);
        }

        protected override Task<FacebookUserInfo> GetUserInfoFromContext(OAuthCreatingTicketContext ctx)
        {
            return Task.FromResult(new FacebookUserInfo
            {
                RefreshToken = ctx.AccessToken,
                Email = ctx.GetEmail(),
                FacebookId = ctx.GetFacebookId()
            });
        }
    }

    public class FacebookUserInfo
    {
        public string RefreshToken { get; set; }

        public string Email { get; set; }

        public string FacebookId { get; set; }
    }
}