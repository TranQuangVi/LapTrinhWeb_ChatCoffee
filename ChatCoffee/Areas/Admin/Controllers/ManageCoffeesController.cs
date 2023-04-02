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
    
    public class ManageCoffeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ManageCoffees
        public ActionResult Index(int ? page)
        {
            if (page == null) page = 1;
            //var all_sach = (from s in db.COFFEEs select s).OrderBy(m => m.MACF);
            int pageSize = 7;
            int pageNum = page ?? 1;
            var cOFFEEs = db.COFFEEs.Include(c => c.LOAISANPHAM).Include(c => c.THUONGHIEU).Include(c => c.ANHs).OrderBy(m => m.MACF);
            return View(cOFFEEs.ToPagedList(pageNum, pageSize));
            
            //return View(cOFFEEs.ToList());
        }

        // GET: Admin/ManageCoffees/Create
        public ActionResult Create()
        {
            ViewBag.MALOAI = new SelectList(db.LOAISANPHAMs, "MALOAI", "TENLOAI");
            ViewBag.MATH = new SelectList(db.THUONGHIEUs, "MATH", "TENTH");
            return View();
        }

        // POST: Admin/ManageCoffees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MACF,TENCF,GIA,SOLUONG,ViewCount,SLDABAN,KHOILUONG,XUATXU,HSD,DANGCF,MOTA,MALOAI,MATH")] COFFEE cOFFEE)
        {
            if (ModelState.IsValid)
            {
                cOFFEE.ViewCount = 0;
                cOFFEE.SLDABAN = 0;
                db.COFFEEs.Add(cOFFEE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MALOAI = new SelectList(db.LOAISANPHAMs, "MALOAI", "TENLOAI", cOFFEE.MALOAI);
            ViewBag.MATH = new SelectList(db.THUONGHIEUs, "MATH", "TENTH", cOFFEE.MATH);
            return View(cOFFEE);
        }

        // GET: Admin/ManageCoffees/Edit/5
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

        // POST: Admin/ManageCoffees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MACF,TENCF,GIA,SOLUONG,ViewCount,SLDABAN,KHOILUONG,XUATXU,HSD,DANGCF,MOTA,MALOAI,MATH")] COFFEE cOFFEE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOFFEE).State = EntityState.Modified;
                UpdateModel(cOFFEE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MALOAI = new SelectList(db.LOAISANPHAMs, "MALOAI", "TENLOAI", cOFFEE.MALOAI);
            ViewBag.MATH = new SelectList(db.THUONGHIEUs, "MATH", "TENTH", cOFFEE.MATH);
            return View(cOFFEE);
        }

        // GET: Admin/ManageCoffees/Delete/5
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

        // POST: Admin/ManageCoffees/Delete/5
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
