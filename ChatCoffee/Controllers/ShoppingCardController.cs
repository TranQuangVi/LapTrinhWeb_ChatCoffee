using ChatCoffee.Models.ModelsDefault;
using ChatCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatCoffee.Models.ModelViews;
using CTGIOHANG = ChatCoffee.Models.ModelViews.CTGIOHANG;
using COFFEE = ChatCoffee.Models.ModelViews.COFFEE;
using GIOHANG = ChatCoffee.Models.ModelViews.GIOHANG;
using HOADON = ChatCoffee.Models.ModelViews.HOADON;
using CTDONHANG = ChatCoffee.Models.ModelViews.CTDONHANG;
using VANCHUYEN = ChatCoffee.Models.ModelViews.VANCHUYEN;
using System.Net.Http;
using System.Web.UI;
using Microsoft.Ajax.Utilities;

namespace ChatCoffee.Controllers
{
    public class ShoppingCardController : Controller
    {
        // lấy id khách hàng theo user name
        /*public void GetIdUserByName(AspNetUser user, string name)
        {
            user = model.AspNetUsers.Single(u => u.UserName.Equals(name));

        }
        public void GetSessionGH(string name)
        {
            AspNetUser user = new AspNetUser();
            GetIdUserByName(user, name);

            int MaGH = (int)model.GIOHANGs.Where(x => x.Id.Equals(user.Id)).FirstOrDefault().MAGH;
            // lưu id giỏ hàng vào Session
            Session["MaGH"] = MaGH;
        }
*/
        public MyDataDataContext model = new MyDataDataContext();
        // GET: Default
        public List<CTGIOHANG> GetListProductInCard(int MaGH)
        {
            List<CTGIOHANG> list = new List<CTGIOHANG>();
            list = model.CTGIOHANGs.Where(x => x.GIOHANG.MAGH.Equals(MaGH)).ToList();
            if (list == null)
                list = new List<CTGIOHANG>();
            return list;
        }

        public ActionResult Index()
        {
            if (Session["MaGH"] == null)
                return RedirectToAction("Login", "Account");
            List<CTGIOHANG> list = GetListProductInCard((int)Session["MaGH"]);
            GIOHANG gh = model.GIOHANGs.Single(n => n.MAGH == (int)Session["MaGH"]);

            // lấy session[soluongcon] 
            // check session[soluongcon] != 0
            // if(session[soluongcon] ==0)

            ViewBag.SumQuantity = SumQuantity(gh);
            ViewBag.SumPrice = SumPrice(gh);
            ViewBag.SumSP = SumSP(gh);
            ViewBag.listGH = list;
            ViewBag.result = "";
            return View();
            // lấy danh sách sản phẩm có trong giỏ hàng của khách hàng
        }


        //Thêm sp vào giỏ hàng
        public ActionResult AddSP(int maSP, string strURL, FormCollection collection)
        {
            int sl = 1;
            if (collection["txtSoLgMua"] != null)
                sl = int.Parse(collection["txtSoLgMua"].ToString());
            if (Session["MaGH"] == null)
            {
                Session["success"] = "false";
                return View();
            }

            Session["success"] = "success";
            List<CTGIOHANG> list = GetListProductInCard((int)Session["MaGH"]);
            CTGIOHANG sp = list.Find(b => b.MACF.Equals(maSP));
            GIOHANG gh = model.GIOHANGs.Single(g => g.MAGH.Equals((int)Session["MaGH"]));
            if (sp == null)
            {
                sp = new CTGIOHANG();
                sp.MACF = maSP;
                sp.MAGH = (int)Session["MaGH"];
                COFFEE cf = model.COFFEEs.Single(n => n.MACF == maSP);
                sp.SOLUONG = sl;
                sp.GIA = cf.GIA;
                sp.TONGGIA = (sp.GIA * sp.SOLUONG);
                list.Add(sp);
                model.CTGIOHANGs.InsertOnSubmit(sp);
                //     model.SubmitChanges();
                //    return Redirect(strURL);
            }
            else
            {
                sp.SOLUONG += sl;

                sp.TONGGIA = (sp.GIA * sp.SOLUONG);
                //     model.SubmitChanges();
                // return RedirectToAction("View", "coffees");

                //    return Redirect(strURL);

            }

            ViewBag.SumQuantity = SumQuantity(gh);
            ViewBag.SumPrice = SumPrice(gh);
            ViewBag.SumSP = SumSP(gh);
            model.SubmitChanges();
            return Redirect(strURL);
        }

        // tính tổng số lượng
        public int SumQuantity(GIOHANG gh)
        {
            List<CTGIOHANG> list = GetListProductInCard((int)Session["MaGH"]);
            int sum = 0;
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.SOLUONG <= item.COFFEE.SOLUONG && item.COFFEE.TRANGTHAI == true)
                        sum += (int)item.SOLUONG;
                }
            }
            model.SubmitChanges();
            gh.TONGSL = sum;
            return sum;
        }
        //tính tổng sp
        public int SumSP(GIOHANG gh)
        {
            List<CTGIOHANG> list = GetListProductInCard((int)Session["MaGH"]);
            int sum = 0;
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.SOLUONG <= item.COFFEE.SOLUONG && item.COFFEE.TRANGTHAI == true)
                        sum += 1;
                }
            }
            gh.TONGSP = sum;
            model.SubmitChanges();

            return sum;
        }

        //tính tổng tiền
        public double SumPrice(GIOHANG gh)
        {
            double sum = 0;
            List<CTGIOHANG> list = GetListProductInCard((int)Session["MaGH"]);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.SOLUONG <= item.COFFEE.SOLUONG && item.COFFEE.TRANGTHAI == true)
                        sum += (double)item.TONGGIA;
                }
            }
            model.SubmitChanges();
            gh.TONGTIEN = sum;
            return sum;
        }

        public ActionResult DeleteGioHang(int maCF)
        {

            CTGIOHANG cartDetail =
               model.CTGIOHANGs
               .Where(x => x.GIOHANG.MAGH == (int)Session["MaGH"])
               .Where(c => c.MACF == maCF)
               .FirstOrDefault();

            if (cartDetail != null)
            {
                model.CTGIOHANGs.DeleteOnSubmit(cartDetail);
                model.SubmitChanges();
                return RedirectToAction("Index", "ShoppingCard");
            }
            return RedirectToAction("Index", "ShoppingCard");
        }

        public ActionResult UpdateGioHang(int maCF, FormCollection collection)
        {
            List<CTGIOHANG> list = GetListProductInCard((int)Session["MaGH"]);
            CTGIOHANG gh = list.SingleOrDefault(b => b.MACF.Equals(maCF));
            if (gh != null)
            {
                gh.SOLUONG = int.Parse(collection["txtSoLg"].ToString());
                gh.TONGGIA = gh.SOLUONG * gh.GIA;
                model.SubmitChanges();
            }

            return RedirectToAction("Index", "ShoppingCard");
        }

        public ActionResult DeleteAll()
        {
            List<CTGIOHANG> list = GetListProductInCard((int)Session["MaGH"]);
            model.CTGIOHANGs.DeleteAllOnSubmit(list);
            model.SubmitChanges();
            return RedirectToAction("Index", "ShoppingCard");
        }


        // tạo đơn hàng từ giỏ hàng
        // lấy thông tin từ giỏ hàng bỏ vào đơn hàng
        // MAHD - tự sinh
        // Id = Id
        // TONGGIA = TONGTIEN
        // lấy MAVT, MATT từ view
        // ngày đặt-nhận hàng như bài trước

        // lấy danh sách sp có trong CTGT --> CTHD
        // lấy MACF, SL, GIA từ CTGH

        // tạo session[soluongcon] 
        // nếu soluong cua session[soluongcon]  ==0 thì ko thêm vòa CTDH
        //

        // Lấy MAHD từ HOADON
        [HttpGet]
        public ActionResult Order()
        {
            if (Session["MaGH"] == null)
            {
                return RedirectToAction("Index", "Coffees");
            }
            HOADON hoadon = new HOADON();
            //   List<CTGIOHANG> lstGiohang = GetListProductInCard((int)Session["MaGH"]);
            List<CTGIOHANG> lstGiohang = model.CTGIOHANGs.
                Where(c => c.MAGH == (int)Session["MaGH"]
                && c.SOLUONG <= c.COFFEE.SOLUONG
                && c.COFFEE.TRANGTHAI == true).ToList();
            GIOHANG giohang = model.GIOHANGs.Single(g => g.MAGH.Equals((int)Session["MaGH"]));

            ViewBag.SumQuantity = SumQuantity(giohang);
            ViewBag.SumPrice = SumPrice(giohang);
            ViewBag.SumSP = SumSP(giohang);
            ViewBag.listGH = lstGiohang;
            ViewBag.VanChuyen = GetListVanChuyen();
             
             var   user = model.AspNetUsers.Where(u => u.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            ViewBag.user = user;
            ViewBag.MAVT = new SelectList(model.VANCHUYENs, "MAVT", "TENVT");
            ViewBag.MATT = new SelectList(model.THANHTOANs, "MATT", "PHUONGTHUC");
            var diachi = model.DIACHIs.Where(d => d.Id.Equals(user.Id));
            ViewBag.DIACHIGIAO = diachi;
          //  ViewBag.DIACHIGIAO = new SelectList(diachi, "MADC", "DIACHIGIAO");

            return View(hoadon);
        }

        public ActionResult Order([Bind(Include = "MAGH, Id,TONGSP,TONGSL,MAVT,MATT ,TONGTIEN,NGAYDAT, NGAYGIAO,DIACHIGIAO,SDTDAT, TRANGTHAI")]
                                    HOADON hoadon)
        {
            // lấy thông tin khách hàng từ 
            //MAHD
            GIOHANG gh = model.GIOHANGs.Single(n => n.MAGH == (int)Session["MaGH"]);
            List<CTGIOHANG> list = GetListProductInCard(gh.MAGH);

            //vận chuyển
            VANCHUYEN vc = model.VANCHUYENs.FirstOrDefault(c => c.MAVT == hoadon.MAVT);
            hoadon.Id = gh.Id;
            hoadon.TONGDONGIA = gh.TONGTIEN + vc.GIA;
            hoadon.TRANGTHAI = "Chờ";
/*            hoadon.DIACHIGIAO = "abc";
            hoadon.SDTDAT = "";*/
            //MAVT
            //MATT
            hoadon.NGAYDAT = DateTime.Now;
            if (hoadon.MAVT == 1)
            {
                hoadon.NGAYGIAO = DateTime.Now.AddDays(+5);
            }
            else if (hoadon.MAVT == 2)
                hoadon.NGAYGIAO = DateTime.Now.AddDays(+2);
            model.HOADONs.InsertOnSubmit(hoadon);
            model.SubmitChanges();
            foreach (var item in list)
            {
                if (item.SOLUONG <= item.COFFEE.SOLUONG && item.COFFEE.TRANGTHAI == true)
                {
                    CTDONHANG ctdh = new CTDONHANG();
                    COFFEE cf = model.COFFEEs.Where(c => c.MACF == item.MACF).FirstOrDefault();
                    ctdh.MAHD = hoadon.MAHD;
                    ctdh.MACF = item.MACF;
                    ctdh.GIA = item.TONGGIA;
                    ctdh.SOLUONG = item.SOLUONG;
                    cf.SOLUONG -= item.SOLUONG;
                    cf.SLDABAN += item.SOLUONG;
                    model.CTDONHANGs.InsertOnSubmit(ctdh);
                    model.SubmitChanges();
                }
            }
            model.CTGIOHANGs.DeleteAllOnSubmit(list);
            model.SubmitChanges();
            return RedirectToAction("DetailHoaDon", "User", new { @MAHD = hoadon.MAHD });
        }

        public List<VANCHUYEN> GetListVanChuyen()
        {
            return model.VANCHUYENs.ToList();
        }

        public ActionResult XacnhanDonhang()
        {
            return View();
        }





    }
}


