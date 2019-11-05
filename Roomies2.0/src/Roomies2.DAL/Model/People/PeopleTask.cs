using System;

namespace Roomies2.DAL.Model.People
{
    public class PeopleTask
    {
        public PeopleTask(int taskId = default, string name = null, string description = null, bool state = default)
        {
            TaskId = taskId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            State = state;
        }

        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
    }
}