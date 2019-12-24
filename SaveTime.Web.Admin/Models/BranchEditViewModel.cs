using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SaveTime.Web.Admin.Models
{
    public class BranchEditViewModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime EndWork { get; set; }
        public int CompanyId { get; set; }
        public List<SelectListItem> Companies { get; set; } = new List<SelectListItem>();
    }
}