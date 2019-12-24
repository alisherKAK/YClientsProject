using SaveTime.AbstractModels.Marker;
using SaveTime.DataModels.Organization;

namespace SaveTime.DataModels.Business
{
    public class Client : IEntity, IAccountOwner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Account Account { get; set; }
    }
}
