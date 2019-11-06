#region

using System;
using System.Collections.Generic;
using Roomies2.DAL.Model.BuildingManagement;

#endregion

namespace Roomies2.DAL.Model.People
{
    public class Supervisor : Account
    {
        protected Supervisor(List<Building> buildings, int userId = default, string userName = null,
            string email = null, byte[] password = null, string firstName = null, string lastName = null,
            string phone = null, bool sex = default, DateTime birthDate = default, bool isSu = true) : base(userId,
            userName, email, password, firstName, lastName, phone, sex, birthDate, isSu)
        {
            Buildings = buildings;
        }

        public List<Building> Buildings { get; }
    }
}