using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using SaveTime.AbstractModels;
using SaveTime.DataModels.Organization;
using SaveTime.Web.Admin.Models;

namespace SaveTime.Web.Admin.Controllers
{
    public class BranchController : Controller
    {
        private readonly IRepository<Company> _repositoryCompany;
        private readonly IRepository<Branch> _repositoryBranch;
        private readonly IRepository<Barber> _repositoryBarber;
        private readonly IMapper _mapper;
        public BranchController(IRepository<Branch> repositoryBranch, IRepository<Barber> repositoryBarber, IRepository<Company> repositoryCompany, IMapper mapper)
        {
            _repositoryCompany = repositoryCompany;
            _repositoryBranch = repositoryBranch;
            _repositoryBarber = repositoryBarber;
            _mapper = mapper;
        }

        // GET: Branch
        public ActionResult Index()
        {
            var branchViewModels = new List<BranchViewModel>();

            foreach (var branch in _repositoryBranch.GetAll())
            {
                var branchViewModel = _mapper.Map<BranchViewModel>(branch);

                branchViewModels.Add(branchViewModel);
            }

            return View(branchViewModels);
        }

        // GET: Branch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var branch = _repositoryBranch.Get(id);
            if (branch == null)
            {
                return HttpNotFound();
            }

            var branchDetailViewModel = _mapper.Map<BranchDetailViewModel>(branch);

            branchDetailViewModel.Employees = new List<BarberViewModel>();

            foreach(var barber in _repositoryBarber.GetAll().Where(b => b.Branch.Id == branch.Id))
            {
                var barberViewModel = _mapper.Map<BarberViewModel>(barber);

                branchDetailViewModel.Employees.Add(barberViewModel);
            }

            return PartialView("_Details", branchDetailViewModel);
        }

        // GET: Branch/Create
        public ActionResult Create()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach(var company in _repositoryCompany.GetAll())
            {
                var selectListItem = new SelectListItem()
                {
                    Text = $"{company.Name} - {company.City}",
                    Value = company.Id.ToString()
                };

                selectListItems.Add(selectListItem);
            }

            return PartialView("_Create", new BranchCreateViewModel() { Companies = selectListItems});
        }

        // POST: Branch/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address,Phone,Email,StartWork,EndWork,CompanyId")] BranchCreateViewModel branchCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var branch = _mapper.Map<Branch>(branchCreateViewModel);
                branch.Company = _repositoryCompany.Get(branch.Company.Id);

                _repositoryBranch.Add(branch);
                return RedirectToAction("Index");
            }

            return View(branchCreateViewModel);
        }

        // GET: Branch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var branch = _repositoryBranch.Get(id);
            if (branch == null)
            {
                return HttpNotFound();
            }

            var branchEditViewModel = _mapper.Map<BranchEditViewModel>(branch);
            foreach(var company in _repositoryCompany.GetAll())
            {
                var selectListItem = new SelectListItem()
                {
                    Text = $"{company.Name} - {company.City}",
                    Value = company.Id.ToString()
                };

                branchEditViewModel.Companies.Add(selectListItem);
            }

            return PartialView("_Edit", branchEditViewModel);
        }

        // POST: Branch/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,Phone,Email,StartWork,EndWork")] BranchEditViewModel branchEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var branch = _mapper.Map<Branch>(branchEditViewModel);
                branch.Company = _repositoryCompany.Get(branchEditViewModel.CompanyId);

                _repositoryBranch.Update(branch);
                return RedirectToAction("Index");
            }
            return View(branchEditViewModel);
        }

        // GET: Branch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var branch = _repositoryBranch.Get(id);
            if (branch == null)
            {
                return HttpNotFound();
            }

            var branchDeleteViewModel = _mapper.Map<BranchDeleteViewModel>(branch);

            return PartialView("_Delete", branchDeleteViewModel);
        }

        // POST: Branch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var branch = _repositoryBranch.Get(id);
            _repositoryBranch.Delete(branch);
            return RedirectToAction("Index");
        }

        public string GetBranches()
        {
            var branches = _repositoryBranch.GetAll().ToList();
            var jsonBranches = JsonConvert.SerializeObject(branches);
            return jsonBranches;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
