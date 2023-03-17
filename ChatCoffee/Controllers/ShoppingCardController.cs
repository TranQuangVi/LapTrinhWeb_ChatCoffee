using ChatCoffee.Models.ModelsDefault;
using ChatCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatCoffee.Models.ModelViews;
using CTGIOHANG = ChatCoffee.Models.ModelViews.CTGIOHANG;

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
            return model.CTGIOHANGs.Where(x => x.GIOHANG.Id.Equals(IdKH)).ToList();
        }
        public ActionResult Index(string IdKH)
        {
            // bắt đăng nhập trước khi vào giỏ hàng,
            // lấy ID của tài khoản đang đăng nhập đưa vào IdKH
            IdKH = "46609a88-ba49-4c1e-8a39-ea755b19f5a4";

            // lấy danh sách sản phẩm có trong giỏ hàng của khách hàng
            List<CTGIOHANG> list = GetListProductInCard(IdKH);
            ViewBag.listGH = list;
            return View();
        }
    }
}

