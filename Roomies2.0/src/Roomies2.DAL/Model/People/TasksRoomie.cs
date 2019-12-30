using System;

namespace Roomies2.DAL.Model.People
{
    public class TasksRoomie
    {

        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime TaskDate { get; set; }
        public string TaskDes { get; set; }
        public int ColocId { get; set; }
        public bool State { get; set; }

        public int RoomieId { get; set; }

        public string FirstName { get; set; }
    }
}
