using Newtonsoft.Json;
using SaveTime.AbstractModels.Marker;
using System;
using System.Collections.Generic;

namespace SaveTime.DataModels.Organization
{
    public class Branch : IEntity
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime EndWork { get; set; }
        [JsonIgnore]
        public virtual Company Company { get; set; }
        public IList<IEmployee> Employees { get; set; }

        public Branch()
        {
            Employees = new List<IEmployee>();
        }
    }
}
