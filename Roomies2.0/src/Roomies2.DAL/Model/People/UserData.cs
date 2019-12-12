namespace Roomies2.DAL.Model.People
{
    public class UserData
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public byte[] HashedPassword { get; set; }

        public string FacebookId { get; set; }

        public string FacebookRefreshToken { get; set; }

        public string GoogleId { get; set; }

        public string GoogleRefreshToken { get; set; }
    
    }
}
