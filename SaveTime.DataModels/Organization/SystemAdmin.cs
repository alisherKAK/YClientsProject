using SaveTime.AbstractModels.Marker;

namespace SaveTime.DataModels.Organization
{
    public class SystemAdmin : IEmployee, IAccountOwner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Account Account { get; set; }
    }
}
