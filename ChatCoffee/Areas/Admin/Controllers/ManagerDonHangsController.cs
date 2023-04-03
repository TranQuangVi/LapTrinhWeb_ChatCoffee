using ChatCoffee.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatCoffee.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagerDonHangsController : Controller
    {
        public MyDataDataContext data = new MyDataDataContext();
        // GET: Admin/ManagerDonHangs
        public ActionResult Index()
        {

            List<HOADON> hd = data.HOADONs.Include(c => c.CTDONHANGs).ToList();

            var listCTHD = data.CTDONHANGs.ToList();
            ViewBag.CTHD = listCTHD;
            return View(hd);
        }

        public ActionResult DetailHoaDon(int? MAHD)
        {
            List<CTDONHANG> ctdh = data.CTDONHANGs.Where(c => c.MAHD == MAHD).ToList();
            return View(ctdh);
        }

        public ActionResult CapNhatTrangThai(int? MAHD, FormCollection collection)
        {
            var hd = data.HOADONs.FirstOrDefault(c => c.MAHD == MAHD);

            hd.TRANGTHAI = collection["TRANGTHAI"];
            UpdateModel(hd);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}