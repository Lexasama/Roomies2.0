namespace Roomies2.DAL.Model.People.OAuth
{
    public class OAuthGoogle
    {
        public string RefreshToken { get; set; }

        public string UserName {get; set;}

        public string Email { get; set; }

        public string GoogleId { get; set; }
    }
}