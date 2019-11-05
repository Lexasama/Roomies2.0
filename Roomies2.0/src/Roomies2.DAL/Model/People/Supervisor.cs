#region

using System;
using System.Collections.Generic;
using Roomies2.DAL.Model.BuildingManagement;

#endregion

namespace Roomies2.DAL.Model.People
{
    public class Supervisor
    {
        public Supervisor(List<Building> buildings = null)
        {
            Buildings = buildings ?? throw new ArgumentNullException(nameof(buildings));
        }

        public List<Building> Buildings { get; }
    }
}