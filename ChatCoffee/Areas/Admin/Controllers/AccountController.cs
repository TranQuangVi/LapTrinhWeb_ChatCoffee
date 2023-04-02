using ChatCoffee.Models;
using ChatCoffee.Models.ModelsDefault;
using ChatCoffee.Models.ModelViews;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ChatCoffee.Areas.Admin.Controllers
{
    
    public class AccountController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        MyDataDataContext data = new MyDataDataContext();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin/Account
        public ActionResult Users(int ? page)
        {

            //var items = data.AspNetUserRoles.Include(c => c.AspNetUser).Where(x => x.RoleId == "90ecb062-774f-4464-adf5-09a1cfc52e1d");
           // lấy danh sách chi teiets  role
           var CTrole = data.AspNetUserRoles.ToList();

            //lấy use usr có role là user
            var user = data.AspNetUsers.ToList().OrderBy(m => m.Id);
            List<AspNetUser> users = new List<AspNetUser>();    
            foreach( var u in user)
            {
                foreach(var r in CTrole)
                {
                    if (r.RoleId.Equals("90ecb062-774f-4464-adf5-09a1cfc52e1d") && r.UserId.Equals(u.Id))
                    {
                        users.Add(u);
                    }
                }
            }
            if (page == null) page = 1;
            //var users1 = (from s in db.Users select s).OrderBy(m => m.Id);
            int pageSize = 7;
            int pageNum = page ?? 1;

            return View(users.ToPagedList(pageNum, pageSize));


        }

        public ActionResult Index(int ? page)
        {

            //var items = data.AspNetUserRoles.Include(c => c.AspNetUser).Where(x => x.RoleId == "90ecb062-774f-4464-adf5-09a1cfc52e1d");
            // lấy danh sách chi teiets  role
            var CTrole = data.AspNetUserRoles.ToList();

            //lấy use usr có role là user
            var user = data.AspNetUsers.ToList().OrderBy(m => m.Id);
            List<AspNetUser> users = new List<AspNetUser>();
            foreach (var u in user)
            {
                foreach (var r in CTrole)
                {
                    if (r.RoleId.Equals("bb6d30ed-89b2-47ad-9ec0-6b4c24bfdcc2") && r.UserId.Equals(u.Id))
                    {
                        users.Add(u);
                    }
                }
            }

            if (page == null) page = 1;
            //var users1 = (from s in db.Users select s).OrderBy(m => m.Id);
            int pageSize = 7;
            int pageNum = page ?? 1;

            return View(users.ToPagedList(pageNum, pageSize));


        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    Phone = model.Phone,
                    Anh = model.Anh
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, model.Role);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Account");
                }
                AddErrors(result);
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(user);
        //}

        //// POST: Admin/ManageCoffees/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(AspNetUser uSer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(uSer).State = EntityState.Modified;
        //        UpdateModel(uSer);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(uSer);
        //}

        public ActionResult Delete(string id)
        {
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    AspNetUser user = data.AspNetUsers.First(x => x.Id == id);
            //    if (user == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    return View(user);
            //}

            //// POST: Admin/ManageCoffees/Delete/5
            //[HttpPost, ActionName("Delete")]
            //[ValidateAntiForgeryToken]
            //public ActionResult DeleteConfirmed(string id)
            //{
            //    AspNetUser user = data.AspNetUsers.First(x => x.Id == id);
            //    data.AspNetUsers.DeleteOnSubmit(user);
            //    data.SubmitChanges();
            //    return RedirectToAction("Index");
            //}
            var item = db.Users.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletee(string id)
        {

                AspNetUser user = data.AspNetUsers.First(x => x.Id == id);
                data.AspNetUsers.DeleteOnSubmit(user);
                data.SubmitChanges();
                 return RedirectToAction("Index");

        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
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