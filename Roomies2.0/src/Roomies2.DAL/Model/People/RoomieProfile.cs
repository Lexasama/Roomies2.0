using Roomies2.DAL.Model.BuildingManagement;
using System;
using System.Collections.Generic;

namespace Roomies2.DAL.Model.People
{
    public class RoomieProfile
    {
        public int RoomieId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string Phone { get; set; }

        public int Sex { get; set; }

        public DateTime BirthDate { get; set; }

        public string Description { get; set; }

        public string PicPath { get; set; }

        public IEnumerable<ColocData>  ColocList{ get ; set; }
    }
}
