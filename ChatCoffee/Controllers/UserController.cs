using ChatCoffee.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatCoffee.Controllers
{
    public class UserController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            var E_sach = data.AspNetUsers.First(m => m.Id == id);
            return View(E_sach);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_id = data.AspNetUsers.First(m => m.Id == id);
            var E_hoten = collection["hoten"];
            var E_sdt = collection["sodienthoai"];
            var E_email = collection["email"];

            E_id.Id = id;
            if (string.IsNullOrEmpty(E_hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_id.UserName = E_hoten;
                E_id.Phone = E_sdt;
                E_id.UserName = E_email;
                UpdateModel(E_id);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
    }

}