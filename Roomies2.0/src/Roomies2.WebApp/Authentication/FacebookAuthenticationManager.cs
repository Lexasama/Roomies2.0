using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Model.People.OAuth;
using Roomies2.WebApp.Services;

namespace Roomies2.WebApp.Authentication
{
    public class FacebookAuthenticationManager : AuthenticationManager<OAuthFacebook>
    {
        public FacebookAuthenticationManager(UserService userService, UserGateway userGateway)
        {
            UserService = userService;
            UserGateway = userGateway;
        }

        public UserService UserService { get; }
        public UserGateway UserGateway { get; }

        protected override async Task CreateOrUpdateUser(OAuthFacebook userInfo)
        {
            if (userInfo.RefreshToken != null)
            {
                userInfo.UserName = "User" + Guid.NewGuid().ToString().Substring(10);

                await UserGateway.CreateOrUpdateFacebookUser(userInfo.UserName, userInfo.Email, userInfo.FacebookId,
                    userInfo.RefreshToken);
            }
        }


        protected override Task<UserData> FindUser(OAuthFacebook userInfo)
        {
            return UserGateway.FindByFacebookId(userInfo.FacebookId);
        }

        protected override Task<OAuthFacebook> GetUserInfoFromContext(OAuthCreatingTicketContext ctx)
        {
            return Task.FromResult(new OAuthFacebook
            {
                RefreshToken = ctx.AccessToken,
                Email = ctx.GetEmail(),
                FacebookId = ctx.GetFacebookId()
            });
        }
    }
}