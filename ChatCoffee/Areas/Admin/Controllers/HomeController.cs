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
    }
}