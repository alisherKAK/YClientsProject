using SaveTime.AbstractModels.Marker;
using SaveTime.DataModels.Organization;
using System;

namespace SaveTime.DataModels.Business
{
    public class Record : IEntity
    {
        public int Id { get; set; }
        public virtual Barber Barber { get; set; }
        public virtual Client Client { get; set; }
        public DateTime BookingTime { get; set; }
        public TimeSpan SpendTime { get; set; }
    }
}
