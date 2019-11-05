#region

using System;
using System.Collections.Generic;

#endregion

namespace Roomies2.DAL.Model.BuildingManagement
{
    public class Building
    {
        public Building(int buildingId = default, string buildingName = null, List<Colocation> colocations = null)
        {
            BuildingId = buildingId;
            BuildingName = buildingName ?? throw new ArgumentNullException(nameof(buildingName));
            Colocations = colocations ?? throw new ArgumentNullException(nameof(colocations));
        }

        public int BuildingId { get; set; }
        public string BuildingName { get; set; }
        public List<Colocation> Colocations { get; set; }
    }
}