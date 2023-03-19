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

namespace ChatCoffee.Controllers
{
    public class ShoppingCardController : Controller
    {
        // GET: ShoppongCard
        //    ApplicationDbContext model = new ApplicationDbContext();

        public MyDataDataContext model = new MyDataDataContext();
        // GET: Default
        public List<CTGIOHANG> GetListProductInCard(string IdKH)
        {
            List<CTGIOHANG> list = new List<CTGIOHANG>();
            list = model.CTGIOHANGs.Where(x => x.GIOHANG.Id.Equals(IdKH)).ToList();
            if (list == null)
            {
                list = new List<CTGIOHANG>();Session["GioHang"] = list;
            }
            return list;
        }


        public ActionResult Index(string IdKH)
        {
            // bắt đăng nhập trước khi vào giỏ hàng,
            // lấy ID của tài khoản đang đăng nhập đưa vào IdKH
            IdKH = "fe86a5f4-27a3-4204-8a7c-98d04fc56892";

            // lấy danh sách sản phẩm có trong giỏ hàng của khách hàng
            List<CTGIOHANG> list = GetListProductInCard(IdKH);

            ViewBag.SumQuantity = SumQuantity(1);
            ViewBag.SumPrice = SumPrice(1);
            ViewBag.SumSP = SumSP(1);
            ViewBag.listGH = list;
            return View();
        }
        //Thêm sp vào giỏ hàng
        public ActionResult AddSP(int maSP, int maGH, string strURL)
        {
            string maKH = "fe86a5f4-27a3-4204-8a7c-98d04fc56892";
            List<CTGIOHANG> list = GetListProductInCard(maKH);
            CTGIOHANG sp = list.Find(b => b.MACF.Equals(maSP));
            if (sp == null)
            {
                sp = new CTGIOHANG();
                sp.MACF = maSP;
                sp.MAGH = maGH;
                COFFEE cf = model.COFFEEs.Single(n => n.MACF == maSP);
                GIOHANG gh = model.GIOHANGs.Single(n => n.MAGH == maGH);
                sp.SOLUONG = 1;
                sp.GIA = cf.GIA;
                sp.TONGGIA = (sp.GIA * sp.SOLUONG);
                list.Add(sp);
                model.CTGIOHANGs.InsertOnSubmit(sp);
                model.SubmitChanges();
                return Redirect(strURL);
            }
            else
            {
                sp.SOLUONG++;
                sp.TONGGIA = (sp.GIA * sp.SOLUONG);
                model.SubmitChanges();
                return Redirect(strURL);
            }
        }

        // tính tổng số lượng
        public int SumQuantity(int maGH)
        {
            string maKH = "fe86a5f4-27a3-4204-8a7c-98d04fc56892";
            List<CTGIOHANG> list = GetListProductInCard(maKH);
            int sum = 0;
            if (list != null)
                sum = (int)list.Sum(b => b.SOLUONG);
            return sum;
        }
        //tính tổng sp
        public int SumSP(int maGH)
        {
            string maKH = "fe86a5f4-27a3-4204-8a7c-98d04fc56892";
            List<CTGIOHANG> list = GetListProductInCard(maKH);
            int sum = 0;
            if (list != null)
            {
                sum = list.Count;
            }
            return sum;
        }

        //tính tổng tiền
        public double SumPrice(int maGH)
        {
            double sum = 0;
            string maKH = "fe86a5f4-27a3-4204-8a7c-98d04fc56892";
            List<CTGIOHANG> list = GetListProductInCard(maKH);
            if (list != null)
            {
                sum = (double)list.Sum(b => b.TONGGIA);
            }
            return sum;
        }

        public ActionResult DeleteGioHang(string maKH, int maCF)
        {
            maKH = "fe86a5f4-27a3-4204-8a7c-98d04fc56892";

            CTGIOHANG cartDetail =
               model.CTGIOHANGs
               .Where(x => x.GIOHANG.Id.Equals(maKH))
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

        public ActionResult UpdateGioHang(string maKH, int maCF, FormCollection collection)
        {
            maKH = "fe86a5f4-27a3-4204-8a7c-98d04fc56892";
            List<CTGIOHANG> list = GetListProductInCard(maKH);
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
            List<CTGIOHANG> list = new List<CTGIOHANG>();
            list.Clear();
            model.SubmitChanges();
            return RedirectToAction("Index", "ShoppingCard");
        }


    }
}

