using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SaveTime.Web.Admin.Models
{
    public class BarberEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime WorkDayStart { get; set; }
        public DateTime WorkDayEnd { get; set; }
        public int BranchId { get; set; }
        public int AccountId { get; set; }
        public List<SelectListItem> Branches { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Accounts { get; set; } = new List<SelectListItem>();
    }
}