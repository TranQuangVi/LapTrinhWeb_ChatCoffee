using ChatCoffee.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using DIACHI = ChatCoffee.Models.ModelViews.DIACHI;

namespace ChatCoffee.Controllers
{
    public class UserController : Controller
    {
        public MyDataDataContext data = new MyDataDataContext();
        // GET: User
        public ActionResult Index()
        {
            //var user = data.AspNetUsers.Single(u => u.UserName.Equals(User.Identity.Name));
            var user = GetUserByUserName();
            if(user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(user);
        }

/*        public ActionResult Edit(string id)
        {
            var E_sach = data.AspNetUsers.First(m => m.Id == id);
            return  RedirectToAction("Index");
        }*/
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_id = data.AspNetUsers.First(m => m.Id == id);
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
                if (E_ngaysinh!=null)
                    E_id.NGAYSINH = DateTime.Parse(E_ngaysinh);

                UpdateModel(E_id);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
           // return this.Edit(id);
            return RedirectToAction("Index");
        }

        public ActionResult QuanLyHoaDon()
        {
            var user = GetUserByUserName();
            List<HOADON> hd = new List<HOADON>();
            ViewBag.listHD = GetHDByKH(user.Id);
            hd = GetHDByKH(user.Id);
            ViewBag.TenCF = data.CTDONHANGs.Where(c => c.HOADON.Id.Equals(user.Id)).ToList();
            return View(hd);
        }

        public List<HOADON> GetHDByKH(string id)
        {
            return data.HOADONs.Where(h=>h.Id.Equals(id)).ToList();
        }


        public ActionResult DetailHoaDon(int? MAHD)
        {
            List<CTDONHANG> ctdh = data.CTDONHANGs.Where(c => c.MAHD == MAHD).ToList();
            return View(ctdh);
        }

        public ActionResult IndexDiaChi()
        {
            var user = GetUserByUserName();
            List<DIACHI> listDC = new List<DIACHI>();
            listDC = data.DIACHIs.Where(c => c.Id.Equals(user.Id)).ToList();
            ViewBag.dc = listDC;
            return View(listDC);
        }

        public AspNetUser GetUserByUserName()
        {
            var user =  data.AspNetUsers.
                Where(u => u.UserName.Equals(User.Identity.Name)).
                FirstOrDefault();
            return user;
        }

        public ActionResult CreateDiaChi(FormCollection collection)
        {
            var diachi = new DIACHI();
            var user = GetUserByUserName();
            diachi.Id = user.Id;
            diachi.TENDC = collection["NewNameDC"];
            diachi.AspNetUser = user;
            data.DIACHIs.InsertOnSubmit(diachi);
            data.SubmitChanges();
            return RedirectToAction("IndexDiaChi");
        }

        public ActionResult EditDiaChi(int? MADC, FormCollection collection)
        {
            var diachi = data.DIACHIs.First(c => c.MADC == MADC);
            diachi.TENDC = collection["TENDC"];
            //dc.AspNetUser = diachi.AspNetUser;
            UpdateModel(diachi);
            data.SubmitChanges();
            return RedirectToAction("IndexDiaChi");
        }
        
        public ActionResult Delete(int? MADC)
        {

            Models.ModelViews.DIACHI dc = data.DIACHIs.First(c => c.MADC == MADC);

            data.DIACHIs.DeleteOnSubmit(dc);
            data.SubmitChanges();
            return RedirectToAction("IndexDiaChi");
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