using ChatCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatCoffee.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext data = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuCategory()
        {
            var items = data.LOAISANPHAMs.ToList();
            return PartialView("_MenuCategory", items);
        }
        public ActionResult MenuView(int? id)
        {
            if(id != null)
            {
                ViewBag.MaLoai = id;
            }    
            var items = data.LOAISANPHAMs.ToList();
            return PartialView("_MenuView", items);
        }
        public ActionResult MenuLocal()
        {

            var items = data.THUONGHIEUs.ToList();
            return PartialView("_MenuLocal", items);
        }
        
    }
}