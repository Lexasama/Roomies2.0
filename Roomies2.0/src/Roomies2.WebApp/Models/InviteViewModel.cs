using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies2.WebApp.Models
{
    public class InviteViewModel
    {
        [Required]
        public IEnumerable<string> Emails { get; set; }

        [Required]
        public int ColocId { get; set; }
    }
}
