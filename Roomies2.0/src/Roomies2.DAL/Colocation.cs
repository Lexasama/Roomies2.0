using System;
using System.Collections.Generic;
using Roomies2.DAL.People;

namespace Roomies2.DAL
{
    public class Colocation
    {
        public Colocation()
        {
            Roomies = new List<Roomie>();
        }

        public int ColocationId { get; set; }
        public string ColocationName { get; set; }
        public string Photo { get; set; }
        public DateTime CreationDate { get; set; }
        public int AdminId { get; set; }
        public List<Roomie> Roomies { get; set; }
    }
}