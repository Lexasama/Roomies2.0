using System;
using System.Collections.Generic;
using System.Text;

namespace Roomies2.DAL.Model.People
{
    public class TaskData
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime TaskDate { get; set; }
        public string TaskDes { get; set; }
        public int ColocId { get; set; }
        public bool State { get; set; }
    }
}
