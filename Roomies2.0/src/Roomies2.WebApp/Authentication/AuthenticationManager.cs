using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Roomies2.DAL.Model.People;

namespace Roomies2.WebApp.Authentication
{
    public abstract class AuthenticationManager<TUserInfo>
    {
        public async Task OnCreatingTicket(OAuthCreatingTicketContext ctx)
        {
            TUserInfo userInfo = await GetUserInfoFromContext(ctx);
            await CreateOrUpdateUser(userInfo);
            IAccountData account = await FindUser(userInfo);
            ctx.Principal = CreatePrincipal(account);
        }

        protected abstract Task<TUserInfo> GetUserInfoFromContext(OAuthCreatingTicketContext ctx);

        protected abstract Task CreateOrUpdateUser(TUserInfo userInfo);

        protected abstract Task<IAccountData> FindUser(TUserInfo userInfo);

        ClaimsPrincipal CreatePrincipal(IAccountData account)
        {
            var claims = new List<Claim>
            {
                new Claim( ClaimTypes.NameIdentifier, account.UserId.ToString(), ClaimValueTypes.String ),
                new Claim( ClaimTypes.Email, account.Email )
            };
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthentication.AuthenticationType, ClaimTypes.Email, string.Empty));
            return principal;
        }
    }
}
