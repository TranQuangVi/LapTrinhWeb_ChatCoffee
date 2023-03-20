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
        // lấy id khách hàng theo user name
/*        public void GetIdUserByName(string name)
        {
            AspNetUser user = model.AspNetUsers.Single(u => u.UserName.Equals(name));
            // MaKH = user.Id;
            int MaGH = (int)model.GIOHANGs.Where(x => x.Id.Equals(user.Id)).FirstOrDefault().MAGH;

            // lưu id giỏ hàng vào Session
            Session["MaGH"] = MaGH;
        }*/

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

/*        //tạo giỏ hàng
        public void  Create(string MaKH)
        {
            List<GIOHANG> list =  model.GIOHANGs.Where(x => x.Id.Equals(MaKH)).ToList(); ;
            if (list == null)
                list.Add(new GIOHANG {  Id =MaKH }) ;
        }*/

        public ActionResult Index()
        {
            //     int magh = (int)Session["MaGH"] ;
            // query id user nếu session null
/*            if (Session["MaGH"] == null)
                GetIdUserByName(MaKH);*/
            List<CTGIOHANG> list = GetListProductInCard((int)Session["MaGH"]);
            ViewBag.SumQuantity = SumQuantity();
            ViewBag.SumPrice = SumPrice();
            ViewBag.SumSP = SumSP();
            ViewBag.listGH = list;
            return View();
            // lấy danh sách sản phẩm có trong giỏ hàng của khách hàng
        }
        //Thêm sp vào giỏ hàng
        public ActionResult AddSP(int maSP, string strURL)
        {

/*            if (Session["MaGH"] == null)
                GetIdUserByName(MaKH);*/
            List<CTGIOHANG> list = GetListProductInCard((int)Session["MaGH"]);
            CTGIOHANG sp = list.Find(b => b.MACF.Equals(maSP));

            if (sp == null)
            {
                sp = new CTGIOHANG();
                sp.MACF = maSP;
                sp.MAGH = (int)Session["MaGH"];
                COFFEE cf = model.COFFEEs.Single(n => n.MACF == maSP);
                GIOHANG gh = model.GIOHANGs.Single(n => n.MAGH == (int)Session["MaGH"]);
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
        public int SumQuantity()
        {
            List<CTGIOHANG> list = GetListProductInCard((int)Session["MaGH"]);
            int sum = 0;
            if (list != null)
                sum = (int)list.Sum(b => b.SOLUONG);
            return sum;
        }
        //tính tổng sp
        public int SumSP()
        {
            List<CTGIOHANG> list = GetListProductInCard((int)Session["MaGH"]);
            int sum = 0;
            if (list != null)
            {
                sum = list.Count;
            }
            return sum;
        }

        //tính tổng tiền
        public double SumPrice()
        {
            double sum = 0;
            List<CTGIOHANG> list = GetListProductInCard((int)Session["MaGH"]);
            if (list != null)
            {
                sum = (double)list.Sum(b => b.TONGGIA);
            }
            return sum;
        }

        public ActionResult DeleteGioHang( int maCF)
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

        public ActionResult UpdateGioHang( int maCF, FormCollection collection)
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
            List<CTGIOHANG> list = new List<CTGIOHANG>();
            list.Clear();
            model.SubmitChanges();
            return RedirectToAction("Index", "ShoppingCard");
        }


    }
}

