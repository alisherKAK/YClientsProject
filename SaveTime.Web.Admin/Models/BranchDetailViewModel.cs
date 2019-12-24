using System;
using System.Collections.Generic;

namespace SaveTime.Web.Admin.Models
{
    public class BranchDetailViewModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime EndWork { get; set; }
        public List<BarberViewModel> Employees { get; set; } = new List<BarberViewModel>();
    }
}