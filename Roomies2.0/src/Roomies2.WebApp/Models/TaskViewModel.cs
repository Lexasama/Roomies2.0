using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Roomies2.WebApp.Models
{
    public class TaskViewModel
    {
        [Required]
        public string TaskName { get; set; }

        [Required]
        public DateTime TaskDate { get; set; }

        public string TaskDes { get; set; }

        [Required]
        public IEnumerable<int> Roomies { get; set; }

        [Required]
        public int ColocId { get; set; }

    }
}
