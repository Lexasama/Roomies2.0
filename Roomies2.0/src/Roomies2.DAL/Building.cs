using System;
using System.Collections.Generic;
using Roomies2.DAL.People;

namespace Roomies2.DAL
{
    public class Building
    {
        public Building()
        {
            Colocations = new List<Colocation>();
        }

        public int BuildingId { get; set; }
        public string BuildingName { get; set; }
        public List<Colocation> Colocations { get; set; }
    }
}