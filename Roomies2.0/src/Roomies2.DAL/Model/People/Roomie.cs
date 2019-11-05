#region

using System;

#endregion

namespace Roomies2.DAL.Model.People
{
    public class Roomie : Account
    {
        protected Roomie(int userId = default, string userName = null, string email = null, byte[] password = null,
            string firstName = null, string lastName = null, string phone = null, bool sex = default,
            DateTime birthDate = default, string description = null, string profilePicture = null) : base(userId,
            userName, email, password, firstName, lastName, phone, sex, birthDate)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            ProfilePicture = profilePicture ?? throw new ArgumentNullException(nameof(profilePicture));
        }

        public string Description { get; set; }
        public string ProfilePicture { get; set; }
    }
}