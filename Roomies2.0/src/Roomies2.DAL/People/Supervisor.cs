using System.Collections.Generic;

namespace Roomies2.DAL.People
{
    public class Supervisor
    {
        public Supervisor() => Buildings = new List<Building>();

        public List<Building> Buildings { get; }
    }
    
    
}