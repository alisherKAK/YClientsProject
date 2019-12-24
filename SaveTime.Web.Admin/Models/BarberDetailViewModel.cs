using System;

namespace SaveTime.Web.Admin.Models
{
    public class BarberDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime WorkDayStart { get; set; }
        public DateTime WorkDayEnd { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
    }
}