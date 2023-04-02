using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ChatCoffee.Models;
using ChatCoffee.Models.ModelsDefault;
using ChatCoffee.Models.ModelViews;
using PagedList;

namespace ChatCoffee.Areas.Admin.Controllers
{
    
    public class LoaisanphamController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Admin/Loaisanpham
        public ActionResult Index(int ? page)
        {
            if (page == null) page = 1;
            var lOAISANPHAMs = (from s in db.LOAISANPHAMs select s).OrderBy(m => m.MALOAI);
            int pageSize = 7;
            int pageNum = page ?? 1;
          
            return View(lOAISANPHAMs.ToPagedList(pageNum, pageSize));
            //return View(db.LOAISANPHAMs.ToList());
        }

        // GET: Admin/Loaisanpham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.ModelsDefault.LOAISANPHAM lOAISANPHAM = db.LOAISANPHAMs.Find(id);
            if (lOAISANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(lOAISANPHAM);
        }

        // GET: Admin/Loaisanpham/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Loaisanpham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MALOAI,TENLOAI")] Models.ModelsDefault.LOAISANPHAM lOAISANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.LOAISANPHAMs.Add(lOAISANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOAISANPHAM);
        }

        // GET: Admin/Loaisanpham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.ModelsDefault.LOAISANPHAM lOAISANPHAM = db.LOAISANPHAMs.Find(id);
            if (lOAISANPHAM == null)
            {
                return HttpNotFound();
            }
            //ViewBag.MALOAI = new SelectList(db.LOAISANPHAMs, "MALOAI", "TENLOAI", lOAISANPHAM.MALOAI);
            return View(lOAISANPHAM);
        }

        // POST: Admin/Loaisanpham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MALOAI,TENLOAI")] Models.ModelsDefault.LOAISANPHAM cOFFEE)
        {
            //var item = db.LOAISANPHAMs.Find(id);
            //UpdateModel(item);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                db.Entry(cOFFEE).State = EntityState.Modified;

                UpdateModel(cOFFEE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(cOFFEE);

        }

        // GET: Admin/Loaisanpham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.ModelsDefault.LOAISANPHAM lOAISANPHAM = db.LOAISANPHAMs.Find(id);
            if (lOAISANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(lOAISANPHAM);
        }

        // POST: Admin/Loaisanpham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.ModelsDefault.LOAISANPHAM lOAISANPHAM = db.LOAISANPHAMs.Find(id);
            db.LOAISANPHAMs.Remove(lOAISANPHAM);
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