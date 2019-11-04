namespace Roomies2.DAL.People
{
    public class OAuthFacebook
    {
        public OAuthFacebook(string userId)
        {
            UserId = userId;
        }

        private string UserId { get; }
        public string FacebookRefreshToken { get; set; }
        public string FacebookId { get; set; }
    }
}