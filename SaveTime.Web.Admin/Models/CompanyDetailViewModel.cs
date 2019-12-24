using System.Collections.Generic;

namespace SaveTime.Web.Admin.Models
{
    public class CompanyDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<BranchViewModel> Branches { get; set; } = new List<BranchViewModel>();
    }
}