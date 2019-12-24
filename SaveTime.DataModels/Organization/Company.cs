using SaveTime.AbstractModels.Marker;
using System.Collections.Generic;

namespace SaveTime.DataModels.Organization
{
    public class Company : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public IList<Branch> Branches { get; set; }

        public Company()
        {
            Branches = new List<Branch>();
        }
    }
}
