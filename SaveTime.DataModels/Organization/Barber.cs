using SaveTime.AbstractModels.Marker;
using SaveTime.DataModels.Dictionary;
using System;
using System.Collections.Generic;

namespace SaveTime.DataModels.Organization
{
    public class Barber : IEmployee, IAccountOwner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime WorkDayStart { get; set; }
        public DateTime WorkDayEnd { get; set; }
        public IList<Service> Services { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Account Account { get; set; }

        public Barber()
        {
            Services = new List<Service>();
        }
    }
}
