using Microsoft.IdentityModel.Tokens;
using System;

namespace Roomies2.WebApp.Authentication
{
    public class TokenProviderOptions
    {

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; } = TimeSpan.FromMinutes(5);

        public SigningCredentials SigningCredentials { get; set; }
    }
}
