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

namespace ChatCoffee.Controllers
{
    public class COFFEEsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: COFFEEs
        public ActionResult Index()
        {
            var cOFFEEs = db.COFFEEs.Include(c => c.LOAISANPHAM).Include(c => c.THUONGHIEU);
            return View(cOFFEEs.ToList());
        }

        // GET: COFFEEs/Details/5
        public ActionResult Details(int?  id)
        {
            GIOHANG gIOHANG = new GIOHANG();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COFFEE cOFFEE = db.COFFEEs.Find(id);
            if (cOFFEE == null)
            {
                return HttpNotFound();
            }
            return View(cOFFEE);
            /*            var item = db.COFFEEs.Find(id);
                        if (item != null)
                        {
                            db.COFFEEs.Attach(item);
                            item.TENCF = item.TENCF + 1;
                            db.Entry(item).Property(x => x.TENCF).IsModified = true;
                            db.SaveChanges();
                        }

                        return View(item);*/
        }

        // GET: COFFEEs/Create
        public ActionResult Create()
        {
            ViewBag.MALOAI = new SelectList(db.LOAISANPHAMs, "MALOAI", "TENLOAI");
            ViewBag.MATH = new SelectList(db.THUONGHIEUs, "MATH", "TENTH");
            return View();
        }

        // POST: COFFEEs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MACF,TENCF,GIA,SOLUONG,KHOILUONG,XUATXU,HSD,DANGCF,MOTA,MALOAI,MATH")] COFFEE cOFFEE)
        {
            if (ModelState.IsValid)
            {
                db.COFFEEs.Add(cOFFEE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MALOAI = new SelectList(db.LOAISANPHAMs, "MALOAI", "TENLOAI", cOFFEE.MALOAI);
            ViewBag.MATH = new SelectList(db.THUONGHIEUs, "MATH", "TENTH", cOFFEE.MATH);
            return View(cOFFEE);
        }

        // GET: COFFEEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COFFEE cOFFEE = db.COFFEEs.Find(id);
            if (cOFFEE == null)
            {
                return HttpNotFound();
            }
            ViewBag.MALOAI = new SelectList(db.LOAISANPHAMs, "MALOAI", "TENLOAI", cOFFEE.MALOAI);
            ViewBag.MATH = new SelectList(db.THUONGHIEUs, "MATH", "TENTH", cOFFEE.MATH);
            return View(cOFFEE);
        }

        // POST: COFFEEs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MACF,TENCF,GIA,SOLUONG,KHOILUONG,XUATXU,HSD,DANGCF,MOTA,MALOAI,MATH")] COFFEE cOFFEE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOFFEE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MALOAI = new SelectList(db.LOAISANPHAMs, "MALOAI", "TENLOAI", cOFFEE.MALOAI);
            ViewBag.MATH = new SelectList(db.THUONGHIEUs, "MATH", "TENTH", cOFFEE.MATH);
            return View(cOFFEE);
        }

        // GET: COFFEEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COFFEE cOFFEE = db.COFFEEs.Find(id);
            if (cOFFEE == null)
            {
                return HttpNotFound();
            }
            return View(cOFFEE);
        }

        // POST: COFFEEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COFFEE cOFFEE = db.COFFEEs.Find(id);
            db.COFFEEs.Remove(cOFFEE);
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
