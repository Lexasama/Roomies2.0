using System;
using System.Collections.Generic;
using System.Text;

namespace Roomies2.DAL.Model.People
{
    public class TaskSuper
    {
        public TaskSuper(TaskData task, IEnumerable<TaskRoomies> roomies)
        {
            TaskId = task.TaskId;
            TaskName = task.TaskName;
            TaskDate = task.TaskDate;
            TaskDes = task.TaskDes;
            ColocId = task.ColocId;
            State = task.State;
            Roomies = roomies;
        }

        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime TaskDate { get; set; }
        public string TaskDes { get; set; }
        public int ColocId { get; set; }
        public bool State { get; set; }
        public IEnumerable<TaskRoomies> Roomies { get; set; }
    }
}
