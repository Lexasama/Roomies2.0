#region

using System;
using System.Collections.Generic;
using Roomies2.DAL.Model.People;

#endregion

namespace Roomies2.DAL.Model.BuildingManagement
{
    public class Colocation
    {
        public Colocation(int colocationId = default, string colocationName = null, string photo = null,
            DateTime creationDate = default, int adminId = default, List<Roomie> roomies = null)
        {
            ColocationId = colocationId;
            ColocationName = colocationName ?? throw new ArgumentNullException(nameof(colocationName));
            Photo = photo ?? throw new ArgumentNullException(nameof(photo));
            CreationDate = creationDate;
            AdminId = adminId;
            Roomies = roomies ?? throw new ArgumentNullException(nameof(roomies));
        }

        public int ColocationId { get; set; }
        public string ColocationName { get; set; }
        public string Photo { get; set; }
        public DateTime CreationDate { get; set; }
        public int AdminId { get; set; }
        public List<Roomie> Roomies { get; set; }
    }
}