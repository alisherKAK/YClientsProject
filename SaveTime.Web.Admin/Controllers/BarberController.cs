using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using SaveTime.AbstractModels;
using SaveTime.DataModels.Organization;
using SaveTime.Web.Admin.Models;

namespace SaveTime.Web.Admin.Controllers
{
    public class BarberController : Controller
    {
        private readonly IRepository<Company> _repositoryCompany;
        private readonly IRepository<Branch> _repositoryBranch;
        private readonly IRepository<Barber> _repositoryBarber;
        private readonly IRepository<Account> _repositoryAccount;
        private readonly IMapper _mapper;
        public BarberController(IRepository<Barber> repositoryBarber, IRepository<Branch> repositoryBranch, IRepository<Company> repositoryCompany, IRepository<Account> repositoryAccount, IMapper mapper)
        {
            _repositoryCompany = repositoryCompany;
            _repositoryBranch = repositoryBranch;
            _repositoryBarber = repositoryBarber;
            _repositoryAccount = repositoryAccount;
            _mapper = mapper;
        }

        // GET: Barber
        public ActionResult Index()
        {
            var barberViewModels = new List<BarberViewModel>();
            
            foreach(var barber in _repositoryBarber.GetAll())
            {
                var barberViewModel = _mapper.Map<BarberViewModel>(barber);
                var branch = _repositoryBranch.Get(barber.Branch.Id);
                var company = _repositoryCompany.Get(branch.Company.Id);

                barberViewModel.BranchAddress = branch.Address;
                barberViewModel.BranchName = company.Name;
                barberViewModel.Email = branch.Email;
                barberViewModel.Phone = branch.Phone;

                barberViewModels.Add(barberViewModel);
            }

            return View(barberViewModels);
        }

        // GET: Barber/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var barber = _repositoryBarber.Get(id);
            if (barber == null)
            {
                return HttpNotFound();
            }

            var barberDetailViewModel = _mapper.Map<BarberDetailViewModel>(barber);
            var branch = _repositoryBranch.Get(barber.Branch.Id);
            var company = _repositoryCompany.Get(branch.Company.Id);

            barberDetailViewModel.BranchName = company.Name;
            barberDetailViewModel.BranchAddress = branch.Address;
            barberDetailViewModel.Phone = branch.Phone;
            barberDetailViewModel.Email = branch.Email;

            return View(barberDetailViewModel);
        }

        // GET: Barber/Create
        public ActionResult Create()
        {
            var accountSelectListItems = new List<SelectListItem>();
            var branchSelectListItems = new List<SelectListItem>();

            foreach(var account in _repositoryAccount.GetAll())
            {
                var accountSelectListItem = new SelectListItem()
                {
                    Text = $"{account.Login} - {account.Phone} - {account.Email}",
                    Value = account.Id.ToString()
                };

                accountSelectListItems.Add(accountSelectListItem);
            }

            foreach (var branch in _repositoryBranch.GetAll())
            {
                var branchSelectListItem = new SelectListItem()
                {
                    Text = $"{branch.Address} - {branch.Phone}",
                    Value = branch.Id.ToString()
                };

                branchSelectListItems.Add(branchSelectListItem);
            }

            return View(new BarberCreateViewModel() {Accounts = accountSelectListItems, Branches = branchSelectListItems });
        }

        // POST: Barber/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,WorkDayStart,WorkDayEnd")] BarberCreateViewModel barberCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var barber = _mapper.Map<Barber>(barberCreateViewModel);
                barber.Account = _repositoryAccount.Get(barberCreateViewModel.AccountId);
                barber.Branch = _repositoryBranch.Get(barberCreateViewModel.BranchId);

                _repositoryBarber.Add(barber);
                return RedirectToAction("Index");
            }

            return View(barberCreateViewModel);
        }

        // GET: Barber/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var barber = _repositoryBarber.Get(id);
            if (barber == null)
            {
                return HttpNotFound();
            }

            var barberEditViewModel = _mapper.Map<BarberEditViewModel>(barber);
            var accountSelectListItems = new List<SelectListItem>();
            var branchSelectListItems = new List<SelectListItem>();

            foreach (var account in _repositoryAccount.GetAll())
            {
                var accountSelectListItem = new SelectListItem()
                {
                    Text = $"{account.Login} - {account.Phone} - {account.Email}",
                    Value = account.Id.ToString()
                };

                accountSelectListItems.Add(accountSelectListItem);
            }

            foreach (var branch in _repositoryBranch.GetAll())
            {
                var branchSelectListItem = new SelectListItem()
                {
                    Text = $"{branch.Address} - {branch.Phone}",
                    Value = branch.Id.ToString()
                };

                branchSelectListItems.Add(branchSelectListItem);
            }

            barberEditViewModel.Accounts = accountSelectListItems;
            barberEditViewModel.Branches = branchSelectListItems;

            return View(barberEditViewModel);
        }

        // POST: Barber/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,WorkDayStart,WorkDayEnd")] BarberEditViewModel barberEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var barber = _mapper.Map<Barber>(barberEditViewModel);
                barber.Account = _repositoryAccount.Get(barberEditViewModel.AccountId);
                barber.Branch = _repositoryBranch.Get(barberEditViewModel.BranchId);

                _repositoryBarber.Update(barber);
                return RedirectToAction("Index");
            }
            return View(barberEditViewModel);
        }

        // GET: Barber/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var barber = _repositoryBarber.Get(id);
            if (barber == null)
            {
                return HttpNotFound();
            }

            var barberDeleteViewModel = _mapper.Map<BarberDeleteViewModel>(barber);
            var branch = _repositoryBranch.Get(barber.Branch.Id);
            var company = _repositoryCompany.Get(branch.Company.Id);

            barberDeleteViewModel.BranchAddress = branch.Address;
            barberDeleteViewModel.BranchName = company.Name;
            barberDeleteViewModel.Email = branch.Email;
            barberDeleteViewModel.Phone = branch.Phone;

            return View(barberDeleteViewModel);
        }

        // POST: Barber/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var barber = _repositoryBarber.Get(id);
            _repositoryBarber.Delete(barber);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        //db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
