using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaveTime.AbstractModels;
using SaveTime.DataAccess;
using SaveTime.DataModels.Dictionary;
using SaveTime.Web.Admin.Models;

namespace SaveTime.Web.Admin.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IRepository<Service> _repository;
        public ServiceController(IRepository<Service> repository)
        {
            _repository = repository;
        }

        // GET: Service
        public ActionResult Index()
        {
            var serviceViewModels = new List<ServiceViewModel>();

            foreach(var service in _repository.GetAll())
            {
                var serviceViewModel = new ServiceViewModel()
                {
                    Id = service.Id,
                    Price = service.Price,
                    Title = service.Title,
                    ApproximatelySpendTimeInMinutes = service.ApproximatelySpendTimeInMinutes
                };

                serviceViewModels.Add(serviceViewModel);
            }

            return View(serviceViewModels);
        }

        // GET: Service/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = _repository.GetAll().Where(s => s.Id == id).FirstOrDefault();
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Service/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Service/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Price,ApproximatelySpendTimeInMinutes")] ServiceViewModel serviceViewModel)
        {
            if (ModelState.IsValid)
            {
                var service = new Service()
                {
                    Title = serviceViewModel.Title,
                    Price = serviceViewModel.Price,
                    ApproximatelySpendTimeInMinutes = serviceViewModel.ApproximatelySpendTimeInMinutes
                };

                _repository.Add(service);
                return RedirectToAction("Index");
            }

            return View(serviceViewModel);
        }

        // GET: Service/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = _repository.GetAll().Where(s => s.Id == id).FirstOrDefault();
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Service/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Price,ApproximatelySpendTimeInMinutes")] Service service)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(service);
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Service/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = _repository.GetAll().Where(s => s.Id == id).FirstOrDefault();
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = _repository.GetAll().Where(s => s.Id == id).FirstOrDefault();
            _repository.Delete(service);
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
