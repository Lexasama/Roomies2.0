using System;
using System.Collections.Generic;
using System.Text;

namespace Roomies2.DAL.Model.People
{
    public class InviteInfo
    {
        public int ColocId { get; set; }
        public int RoomieId { get; set; }
        public string FirstName { get; set; }
        public string ColocName { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
