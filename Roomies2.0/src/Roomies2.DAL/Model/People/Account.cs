#region

using System;

#endregion

namespace Roomies2.DAL.Model.People
{
    public class Account : IAccountData
    {
        protected Account( int userId = default, string userName = null, string email = null, byte[] password = null,
            string firstName = null, string lastName = null, string phone = null, bool sex = default,
            DateTime birthDate = default, bool isSu = default)
        {
            IsSu = isSu;
            UserId = userId;
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            Sex = sex;
            BirthDate = birthDate;
        }

        public int UserId { get; set; }
        public string UserName { get; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Phone { get; }
        public bool Sex { get; }
        public DateTime BirthDate { get; }
        public bool IsSu { get; }
    }
}