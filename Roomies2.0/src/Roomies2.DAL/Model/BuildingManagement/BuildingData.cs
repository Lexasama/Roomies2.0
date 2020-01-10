namespace Roomies2.DAL.Model.BuildingManagement
{
    public class BuildingData
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; }
        public string BuildingAddress { get; set; }        
        public int SupervisorId { get; set; }
    }
}
