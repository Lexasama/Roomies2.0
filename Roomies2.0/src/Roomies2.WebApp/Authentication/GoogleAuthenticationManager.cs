using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Model.People.OAuth;
using Roomies2.WebApp.Services;

namespace Roomies2.WebApp.Authentication
{
    public class GoogleAuthenticationManager : AuthenticationManager<OAuthGoogle>
    {
        public GoogleAuthenticationManager(UserService userService, UserGateway userGateway)
        {
            UserService = userService;
            Gateway = userGateway;
        }

        public UserService UserService { get; }
        public UserGateway Gateway { get; }

        protected override async Task CreateOrUpdateUser(OAuthGoogle userInfo)
        {
            string userName = Guid.NewGuid().ToString();
            if (userInfo.RefreshToken != null)
                await Gateway.CreateOrUpdateGoogleUser(userName, userInfo.Email, userInfo.GoogleId,
                    userInfo.RefreshToken);
        }

        protected override Task<UserData> FindUser(OAuthGoogle userInfo)
        {
            return Gateway.FindByGoogleId(userInfo.GoogleId);
        }

        protected override Task<OAuthGoogle> GetUserInfoFromContext(OAuthCreatingTicketContext ctx)
        {
            return Task.FromResult(new OAuthGoogle
            {
                RefreshToken = ctx.RefreshToken,
                Email = ctx.GetEmail(),
                GoogleId = ctx.GetGoogleId()
            });
        }
    }
}