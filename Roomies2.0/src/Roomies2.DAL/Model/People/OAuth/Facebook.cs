#region

using System;

#endregion

namespace Roomies2.DAL.Model.People.OAuth
{
    public class OAuthFacebook
    {
        public OAuthFacebook(string userId = null, string facebookRefreshToken = null, string facebookId = null)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            FacebookRefreshToken =
                facebookRefreshToken ?? throw new ArgumentNullException(nameof(facebookRefreshToken));
            FacebookId = facebookId ?? throw new ArgumentNullException(nameof(facebookId));
        }

        public string UserId { get; }
        public string FacebookRefreshToken { get; }
        public string FacebookId { get; }
    }
}