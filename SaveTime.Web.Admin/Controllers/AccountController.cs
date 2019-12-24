using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using SaveTime.AbstractModels;
using SaveTime.DataModels.Organization;
using SaveTime.Web.Admin.Models;

namespace SaveTime.Web.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<Account> _repositoryAccount;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter; 
        public AccountController(IRepository<Account> repositoryAccount, IMapper mapper, IEncrypter encrypter)
        {
            _repositoryAccount = repositoryAccount;
            _mapper = mapper;
            _encrypter = encrypter;
        }

        // GET: Account
        public ActionResult Index()
        {
            var accountViewModels = new List<AccountViewModel>();
            foreach(var account in _repositoryAccount.GetAll())
            {
                var accountViewModel = _mapper.Map<AccountViewModel>(account);

                accountViewModels.Add(accountViewModel);
            }

            return View(accountViewModels);
        }

        // GET: Account/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var account = _repositoryAccount.Get(id);
            if (account == null)
            {
                return HttpNotFound();
            }

            var accountDetailViewModel = _mapper.Map<AccountDetailViewModel>(account);

            return View(accountDetailViewModel);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,Phone,Email,Password")] AccountCreateViewModel accountCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                accountCreateViewModel.Password = _encrypter.HashPassword(accountCreateViewModel.Password);
                var account = _mapper.Map<Account>(accountCreateViewModel);

                _repositoryAccount.Add(account);
                return RedirectToAction("Index");
            }

            return View(accountCreateViewModel);
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var account = _repositoryAccount.Get(id);
            if (account == null)
            {
                return HttpNotFound();
            }

            var accountEditViewModel = _mapper.Map<AccountEditViewModel>(account);

            return View(accountEditViewModel);
        }

        // POST: Account/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Phone,Email,Password")] AccountEditViewModel accountEditViewModel)
        {
            if (ModelState.IsValid)
            {
                accountEditViewModel.Password = _encrypter.HashPassword(accountEditViewModel.Password);
                var account = _mapper.Map<Account>(accountEditViewModel);

                _repositoryAccount.Update(account);
                return RedirectToAction("Index");
            }
            return View(accountEditViewModel);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var account = _repositoryAccount.Get(id);
            if (account == null)
            {
                return HttpNotFound();
            }

            var accountDeleteViewModel = _mapper.Map<AccountDeleteViewModel>(account);
            return View(accountDeleteViewModel);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var account = _repositoryAccount.Get(id);
            _repositoryAccount.Delete(account);
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
