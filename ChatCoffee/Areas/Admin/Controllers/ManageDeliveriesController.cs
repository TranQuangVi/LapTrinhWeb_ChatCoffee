using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ChatCoffee.Models;
using ChatCoffee.Models.ModelsDefault;
using PagedList;

namespace ChatCoffee.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageDeliveriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ManageDeliveries
        public ActionResult Index(int ? page)
        {
            if (page == null) page = 1;
            var vANCHUYENs = (from s in db.VANCHUYENs select s).OrderBy(m => m.MAVT);
            int pageSize = 7;
            int pageNum = page ?? 1;

            return View(vANCHUYENs.ToPagedList(pageNum, pageSize));
        }

        // GET: Admin/ManageDeliveries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VANCHUYEN vANCHUYEN = db.VANCHUYENs.Find(id);
            if (vANCHUYEN == null)
            {
                return HttpNotFound();
            }
            return View(vANCHUYEN);
        }

        // GET: Admin/ManageDeliveries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ManageDeliveries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAVT,TENVT,GIA")] VANCHUYEN vANCHUYEN)
        {
            if (ModelState.IsValid)
            {
                db.VANCHUYENs.Add(vANCHUYEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vANCHUYEN);
        }

        // GET: Admin/ManageDeliveries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VANCHUYEN vANCHUYEN = db.VANCHUYENs.Find(id);
            if (vANCHUYEN == null)
            {
                return HttpNotFound();
            }
            return View(vANCHUYEN);
        }

        // POST: Admin/ManageDeliveries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAVT,TENVT,GIA")] VANCHUYEN vANCHUYEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vANCHUYEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vANCHUYEN);
        }

        // GET: Admin/ManageDeliveries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VANCHUYEN vANCHUYEN = db.VANCHUYENs.Find(id);
            if (vANCHUYEN == null)
            {
                return HttpNotFound();
            }
            return View(vANCHUYEN);
        }

        // POST: Admin/ManageDeliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VANCHUYEN vANCHUYEN = db.VANCHUYENs.Find(id);
            db.VANCHUYENs.Remove(vANCHUYEN);
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
