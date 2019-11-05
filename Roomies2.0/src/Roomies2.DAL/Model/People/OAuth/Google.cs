#region

using System;

#endregion

namespace Roomies2.DAL.Model.People.OAuth
{
    public class OAuthGoogle
    {
        public OAuthGoogle(string userId = null, string googleRefreshToken = null, string googleId = null)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            GoogleRefreshToken = googleRefreshToken ?? throw new ArgumentNullException(nameof(googleRefreshToken));
            GoogleId = googleId ?? throw new ArgumentNullException(nameof(googleId));
        }

        public string UserId { get; }
        public string GoogleRefreshToken { get; }
        public string GoogleId { get; }
    }
}