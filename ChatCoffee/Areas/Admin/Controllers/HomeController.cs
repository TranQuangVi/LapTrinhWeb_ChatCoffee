using ChatCoffee.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatCoffee.Areas.Admin.Controllers
{
    
    public class HomeController : Controller
    {
        public MyDataDataContext db = new MyDataDataContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}