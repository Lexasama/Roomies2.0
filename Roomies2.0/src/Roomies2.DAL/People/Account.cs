using System;

namespace Roomies2.DAL.People
{
    public class Account : IAccountData
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Phone { get; }
        public bool Sex { get; }
        public DateTime BirthDate { get; }
    }
}