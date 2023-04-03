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
using ChatCoffee.Repository;

namespace ChatCoffee.Controllers
{
    public class COFFEEsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public Repository.SearchByOption repository = new Repository.SearchByOption();

        public ActionResult Index(string search)
        {
            if (search == null)
            {
                var cOFFEEs = db.COFFEEs.
                    Include(c => c.LOAISANPHAM).
                    Include(c => c.THUONGHIEU).
                    Include(c => c.ANHs).
                    Where(c => c.TRANGTHAI.Equals(true));

                return View(cOFFEEs.ToList());
            }
            else
            {
                List<COFFEE> query = new List<COFFEE>();
                switch (search)
                {
                    case "Lọc giá giảm":
                        query = repository.LocGiaGiam();
                        return View(query);

                    case "Lọc giá tăng":
                        query = repository.LocGiaTang();
                        return View(query);
                    case "A-Z":
                        query = repository.TuAZ();
                        return View(query);
                    case "Z-A":
                        query = repository.TuZA();
                        return View(query);
                    case "0-500":
                        query = repository.locgia1();
                        return View(query);
                    case "500-1trieu":
                        query = repository.locgia2();
                        return View(query);
                    case "1trieu-2trieu":
                        query = repository.locgia3();
                        return View(query);

                    default:
                        var cOFFEEs = db.COFFEEs
                     .Include(c => c.LOAISANPHAM)
                     .Include(c => c.THUONGHIEU)
                     .Include(c => c.ANHs)
                     .Where(c => c.TENCF.Contains(search) ||
                     c.LOAISANPHAM.TENLOAI.Contains(search) ||
                     c.XUATXU.Contains(search) ||
                     c.DANGCF.Contains(search) ||
                     (c.KHOILUONG.ToString() + "kg").Contains(search) ||
                     (c.KHOILUONG.ToString() + "g").Contains(search) ||
                     c.THUONGHIEU.TENTH.Contains(search) &&
                     c.TRANGTHAI.Equals(true));
                        return View(cOFFEEs);
                }
            }
        }

        
        // GET: COFFEEs/Details/5
        public ActionResult Details(int? id)
        {
            GIOHANG gIOHANG = new GIOHANG();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COFFEE cOFFEE = db.COFFEEs.Find(id);
            if (cOFFEE != null)
            {
                db.COFFEEs.Attach(cOFFEE);
                cOFFEE.ViewCount = cOFFEE.ViewCount + 1;
                db.Entry(cOFFEE).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
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



        public ActionResult CFCategory(string search, int id)
        {
            if (search == null)
            {
                var cOFFEEs = db.COFFEEs
                    .Include(c => c.LOAISANPHAM)
                    .Include(c => c.THUONGHIEU)
                    .Include(c => c.ANHs)
                    .Where(x => x.MALOAI == id && x.TRANGTHAI.Equals(true)).ToList();
                // đây là model view
                ViewBag.MaLoai = id;

                return View(cOFFEEs);
            }
            else
            {
                return RedirectToAction("Index", "coffees", new { @search = search });
            }
        }

        public ActionResult CFLocal(string search, int id)
        {
            if (search == null)
            {
                var cOFFEEs = db.COFFEEs
                    .Include(c => c.LOAISANPHAM)
                    .Include(c => c.THUONGHIEU)
                    .Include(c => c.ANHs)
                    .Where(x => x.MATH == id && x.TRANGTHAI.Equals(true)).ToList();
                // đây là model view
                ViewBag.MaTH = id;

                return View(cOFFEEs);
            }
            else
            {
                return RedirectToAction("Index", "coffees", new { @search = search });
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Partial_ItemsByCateId()
        {
            //Where(c => c.TRANGTHAI.Equals(true))
            //var item = db.COFFEEs.ToList();
            var ketqua = db.COFFEEs.Where(x => x.SLDABAN <= int.MaxValue && x.TRANGTHAI.Equals(true)).OrderByDescending(x => x.SLDABAN).Take(10).ToList();
            return PartialView(ketqua);

        }
    }
}
