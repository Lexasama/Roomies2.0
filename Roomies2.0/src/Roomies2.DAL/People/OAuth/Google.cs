namespace Roomies2.DAL.People
{
    public class OAuthGoogle
    {
        public OAuthGoogle(string userId)
        {
            UserId = userId;
        }

        private string UserId { get; }
        public string GoogleRefreshToken { get; set; }
        public string GoogleId { get; set; }
    }
}