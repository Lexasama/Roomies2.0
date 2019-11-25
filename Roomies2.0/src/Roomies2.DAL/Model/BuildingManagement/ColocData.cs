using System;

namespace Roomies2.DAL.Model.BuildingManagement
{
    public class ColocData
    {
        public int ColocId { get; set; }
        public string ColocName { get; set; }
        public DateTime CreationDate { get; set; }

        public string PicPath { get; set; }
    }
}