using ChatCoffee.Models.ModelViews;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatCoffee.Areas.Admin.Controllers
{
    [Authorize( Roles = "Admin")]
    public class HomeController : Controller
    {
        public MyDataDataContext db = new MyDataDataContext();
        // GET: Admin/Home
        public ActionResult Index()
        {

            Session["acc"] = db.AspNetUsers.FirstOrDefault(c => c.UserName.Equals(User.Identity.Name));
            return View();
        }
        public ActionResult Profile()
        {
            //var user = data.AspNetUsers.Single(u => u.UserName.Equals(User.Identity.Name));
            var user = db.AspNetUsers.
                Where(u => u.UserName.Equals(User.Identity.Name)).
                FirstOrDefault();
            Session["acc"] = db.AspNetUsers.FirstOrDefault(c => c.UserName.Equals(User.Identity.Name));
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_id = db.AspNetUsers.First(m => m.Id == id);
            var E_hoten = collection["FULLNAME"];
            var E_sdt = collection["PHONE"];
            var E_email = collection["Email"];
            var E_gioitinh = collection["GIOITINH"];
            var E_ngaysinh = String.Format("{0:yyyy/MM/dd}", collection["NGAYSINH"]);

            E_id.Id = id;
            if (string.IsNullOrEmpty(E_hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_id.FullName = E_hoten;
                E_id.Phone = E_sdt;
                E_id.GIOITINH = E_gioitinh;
                if (E_ngaysinh != null)
                    E_id.NGAYSINH = DateTime.Parse(E_ngaysinh);

                UpdateModel(E_id);
                db.SubmitChanges();
                return RedirectToAction("profile");
            }
            // return this.Edit(id);
            return RedirectToAction("profile");
        }

    }
}