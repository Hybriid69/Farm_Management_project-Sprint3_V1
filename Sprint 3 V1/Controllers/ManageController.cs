using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Sprint_3_V1.App_Start.IdentityConfig;

namespace Sprint_3_V1.Controllers
{
    public class ManageController : Controller
    {
        private AppLogInManager _signInManager;
        private ApplicationUserManager _userManager;
        public enum ManageMessageId
        {
            ChangePasswordSuccess
        }

        // GET: Manage
        [Authorize(Roles = "Admin , Manager")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdminPage()
        {
            return View();
        }
        [Authorize(Roles = "Admin , Manager , Clerk")]
        public ActionResult ClerkPage()
        {
            return View();
        }
        [Authorize(Roles = "Admin , Manager , Human resources , Clerk")]
        public ActionResult HumanResourcePage()
        {
            return View();
        }
        [Authorize(Roles = "Admin , Manager")]
        public ActionResult ManagerPage()
        {
            return View();
        }
        [Authorize(Roles = "Admin , Manager , Clerk , Worker")]
        public ActionResult WorkerPage()
        {
            return View();
        }
        [Authorize(Roles = "Admin , Manager , Clerk , Delivery , Foreman")]
        public ActionResult DeliveryPage()
        {
            return View();
        }
        [Authorize(Roles = "Admin , Manager , Clerk , Customer")]
        public ActionResult CustomerPage()
        {
            return View();
        }
        [AllowAnonymous]
        public class ApplicationUser : IdentityUser
        {
            public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
            {
                // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
                // Add custom user claims here
                return userIdentity;
            }
        }
        [AllowAnonymous]
        public class ApplicationUserManager : UserManager<ApplicationUser>
        {
            public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
            {
            }
        }
        public AppLogInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AppLogInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        //// GET: /Manage/ChangePassword
        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}

        ////
        //// POST: /Manage/ChangePassword
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
        //    if (result.Succeeded)
        //    {
        //        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //       // var pass= UserManager.FindById(User.Identity.GetUserId());
        //        if (user != null)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //        }
        //        return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
        //    }
        //    AddErrors(result);
        //    return View(model);
        //}
        [AllowAnonymous]
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
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

    }
}