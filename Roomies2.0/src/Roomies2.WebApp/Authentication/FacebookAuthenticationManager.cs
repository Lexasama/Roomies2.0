using System.Threading.Tasks;
using Roomies2.DAL;
using Roomies2.WebApp.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Roomies2.DAL.People;

namespace Roomies2.WebApp.Authentication
{
    public class FacebookAuthenticationManager : AuthenticationManager<FacebookUserInfo>
    {
        readonly UserGateway _userGateway;

        public FacebookAuthenticationManager(UserService userService, UserGateway userGateway)
        {
            _userGateway = userGateway;
        }

        protected override async Task CreateOrUpdateUser(FacebookUserInfo userInfo)
        {
            if(userInfo.RefreshToken != null)
            {
                await _userGateway.CreateOrUpdateFacebookUser(userInfo.Email, userInfo.FacebookId, userInfo.RefreshToken);
            }
        }

        protected override Task<IAccountData> FindUser(FacebookUserInfo userInfo)
        {
            return _userGateway.FindByFaceBookId(userInfo.FacebookId);
        }

        protected override Task<FacebookUserInfo> GetUserInfoFromContext(OAuthCreatingTicketContext ctx)
        {
            return Task.FromResult(new FacebookUserInfo
            {
                RefreshToken = ctx.RefreshToken,
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
