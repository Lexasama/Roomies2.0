using System.Threading.Tasks;
using Roomies2.WebApp.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Model.People.OAuth;

namespace Roomies2.WebApp.Authentication
{
    public class FacebookAuthenticationManager : AuthenticationManager<OAuthFacebook>
    {
        public UserService UserService { get; }
        public UserGateway Gateway { get; }

        public FacebookAuthenticationManager(UserService userService, UserGateway userGateway)
        {
            UserService = userService;
            Gateway = userGateway;
        }

        protected override async Task CreateOrUpdateUser(OAuthFacebook userInfo)
        {
            if(userInfo.RefreshToken != null)
            {
                await Gateway.CreateOrUpdateFacebookUser(userInfo.Email, userInfo.FacebookId, userInfo.RefreshToken);
            }
        }

        protected override Task<IAccountData> FindUser(OAuthFacebook userInfo)
        {
            return Gateway.FindByFacebookId(userInfo.FacebookId);
        }

        protected override Task<OAuthFacebook> GetUserInfoFromContext(OAuthCreatingTicketContext ctx)
        {
            return Task.FromResult(new OAuthFacebook
            {
                RefreshToken = ctx.RefreshToken,
                Email = ctx.GetEmail(),
                FacebookId = ctx.GetFacebookId()
            });
        }
    }
}
