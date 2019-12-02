using System;
using System.Collections.Generic;
using System.Text;

namespace Roomies2.DAL.Model.People
{
    public class TaskData
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public string ColocId { get; set; }
    }
}
