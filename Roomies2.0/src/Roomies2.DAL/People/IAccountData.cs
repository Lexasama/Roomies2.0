using System;

namespace Roomies2.DAL.People
{
    public interface IAccountData
    {
        int UserId { get; set; }

        string UserName { get; }
        string Email { get; set; }
        byte[] Password { get; set; }

        string FirstName { get; }
        string LastName { get; }
        string Phone { get; }
        bool Sex { get; }
        DateTime BirthDate { get; }
    }
}