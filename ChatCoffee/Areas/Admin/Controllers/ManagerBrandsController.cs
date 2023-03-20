using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChatCoffee.Models;
using ChatCoffee.Models.ModelsDefault;

namespace ChatCoffee.Areas.Admin.Controllers
{
    public class ManagerBrandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ManagerBrands
        public ActionResult Index()
        {
            return View(db.THUONGHIEUs.ToList());
        }

        // GET: ManagerBrands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagerBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MATH,TENTH")] THUONGHIEU tHUONGHIEU)
        {
            if (ModelState.IsValid)
            {
                db.THUONGHIEUs.Add(tHUONGHIEU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tHUONGHIEU);
        }

        // GET: ManagerBrands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUONGHIEU tHUONGHIEU = db.THUONGHIEUs.Find(id);
            if (tHUONGHIEU == null)
            {
                return HttpNotFound();
            }
            return View(tHUONGHIEU);
        }

        // POST: ManagerBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MATH,TENTH")] THUONGHIEU tHUONGHIEU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHUONGHIEU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tHUONGHIEU);
        }

        // GET: ManagerBrands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUONGHIEU tHUONGHIEU = db.THUONGHIEUs.Find(id);
            if (tHUONGHIEU == null)
            {
                return HttpNotFound();
            }
            return View(tHUONGHIEU);
        }

        // POST: ManagerBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THUONGHIEU tHUONGHIEU = db.THUONGHIEUs.Find(id);
            db.THUONGHIEUs.Remove(tHUONGHIEU);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}