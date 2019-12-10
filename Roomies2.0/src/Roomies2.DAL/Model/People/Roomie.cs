#region

using System;

#endregion

namespace Roomies2.DAL.Model.People
{
    public class Roomie : Account
    {
        protected Roomie(string description, string profilePicture, int userId = default, string userName = null,
            string email = null, byte[] password = null, string firstName = null, string lastName = null,
            string phone = null, bool sex = default, DateTime birthDate = default, bool isSu = false) : base(userId,
            userName, email, password, firstName, lastName, phone, sex, birthDate, isSu)
        {
            Description = description;
            ProfilePicture = profilePicture;
        }

        public string Description { get; set; }
        public string ProfilePicture { get; set; }
    }
}