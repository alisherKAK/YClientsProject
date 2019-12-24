using SaveTime.AbstractModels.Marker;

namespace SaveTime.DataModels.Organization
{
    public class Account : IEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual IAccountOwner AccountOwner { get; set; }
    }
}
