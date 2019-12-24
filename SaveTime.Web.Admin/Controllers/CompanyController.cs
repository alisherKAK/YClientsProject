using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SaveTime.AbstractModels;
using SaveTime.DataAccess;
using SaveTime.DataModels.Organization;
using SaveTime.Web.Admin.Models;

namespace SaveTime.Web.Admin.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IRepository<Company> _repositoryCompany;
        private readonly IRepository<Branch> _repositoryBranch;
        private readonly IMapper _mapper;
        public CompanyController(IRepository<Company> repositoryCompany, IRepository<Branch> repositoryBranch, IMapper mapper)
        {
            _repositoryCompany = repositoryCompany;
            _repositoryBranch = repositoryBranch;
            _mapper = mapper;
        }


        // GET: Company
        public ActionResult Index()
        {
            var companyViewModels = new List<CompanyViewModel>();

            foreach(var company in _repositoryCompany.GetAll())
            {
                var companyViewModel = _mapper.Map<CompanyViewModel>(company);

                companyViewModels.Add(companyViewModel);
            }

            return View(companyViewModels);
        }

        // GET: Company/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = _repositoryCompany.Get(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            var companyDetailViewModel = _mapper.Map<CompanyDetailViewModel>(company);

            var branches = _repositoryBranch.GetAll().Where(b => b.Company.Id == company.Id).ToList();
            var branchesDetailsViewModel = new List<BranchViewModel>();

            foreach(var branch in branches)
            {
                var branchDetailViewModel = _mapper.Map<BranchViewModel>(branch);

                branchesDetailsViewModel.Add(branchDetailViewModel);
            }

            companyDetailViewModel.Branches = branchesDetailsViewModel;

            return View(companyDetailViewModel);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,City")] CompanyCreateViewModel companyCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var company = _mapper.Map<Company>(companyCreateViewModel);

                _repositoryCompany.Add(company);
                return RedirectToAction("Index");
            }

            return View(companyCreateViewModel);
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var company = _repositoryCompany.Get(id);
            if (company == null)
            {
                return HttpNotFound();
            }

            var companyEditViewModel = _mapper.Map<CompanyEditViewModel>(company);

            return View(companyEditViewModel);
        }

        // POST: Company/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,City")] CompanyEditViewModel companyEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var company = _mapper.Map<Company>(companyEditViewModel);

                _repositoryCompany.Update(company);
                return RedirectToAction("Index");
            }
            return View(companyEditViewModel);
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = _repositoryCompany.Get(id);
            if (company == null)
            {
                return HttpNotFound();
            }

            var companyDeleteViewModel = _mapper.Map<CompanyDeleteViewModel>(company);

            return View(companyDeleteViewModel);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var company = _repositoryCompany.Get(id);

            foreach(var branch in _repositoryBranch.GetAll().Where(b => b.Company.Id == company.Id))
            {
                branch.Company = null;
                _repositoryBranch.Update(branch);
            }

            _repositoryCompany.Delete(company);
            return RedirectToAction("Index");
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
