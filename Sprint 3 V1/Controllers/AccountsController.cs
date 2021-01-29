using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sprint_3_V1.Models;
using Sprint_3_V1.ViewModels;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Net.Mail;

namespace Sprint_3_V1.Controllers
{
    public class AccountsController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        // GET: Accounts
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        // GET: Accounts/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountID,UserName,Password,Type,Disabled")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Confirmation(string receiver, string name, string subject, string body)
        { 
            try
            {
            //    if (ModelState.IsValid)
                //{
                    var senderEmail = new MailAddress("farmworks69@gmail.com", "reg");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "farmerbrown1";
                    var sub = subject;
                    var bod = body;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                //}
            }
            catch (Exception)
            {
                ViewBag.Error = "An Error Occurred";
            }
            return View();
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        // register a new customer
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "AccountID,UserName,Password,ConfirmPassword")] Account account)
        {
            account.Type = "Customer";
            string accid = account.UserName;
            string accpass = account.Password;
            TempData["Username"] = accid;

            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                TempData["Username"] = accid;
                TempData["Password"] = accpass;
                LoginModel log = new LoginModel();
                log.Username = accid;
                log.Password = accpass;
                log.Role = "Customer";
                custLogin(log);

               // Account.BuildEmailTemplate("Iqsaanmia@gmail.com","registered");

                return RedirectToAction("CustomerDetails", "Customers");
            }
            return View(account);
        }
        //-----------------------------------------------------------------------------------------------

        // GET: Accounts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,UserName,Password,Type,Disabled")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //-----------------------------------------------------------------------------------------------

        //Log In
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Unauthorised", "Unauthorised");
            }
            return View();
        }

        // Log in 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            //if (ModelState.IsValid)
            // {
            try
            {
                var accID = (from c in db.Accounts
                             where c.UserName == model.Username
                             where c.Password == model.Password
                             select c.AccountID).FirstOrDefault();
                int accountID = Convert.ToInt16(accID);

                var loginInfo = (from c in db.Accounts
                                 where c.UserName == model.Username
                                 where c.Password == model.Password
                                 select new { c.UserName, c.Password }).ToList();

                var accType = (from c in db.Accounts
                               where c.AccountID == accountID
                               select c.Type).Single();

                var positionID = db.Employees.Where(u => u.AccountID == accID).Select(u => u.PositionID).Count();

                if (accType == "Customer" || accType == "customer")
                {
                    var custRole = "Customer";
                    var Custlogindetails = loginInfo.First();

                    SignInUser(Custlogindetails.UserName, custRole, false);

                    return RedirectToLocal(returnUrl);
                   
                }

                else if (accType == "Admin" || accType == "admin")
                {
                    var logindetails = loginInfo.First();
                    string addrole = "Admin";

                    SignInUser(logindetails.UserName, addrole, false);
                    if (returnUrl == null)
                    {
                        return RedirectToAction("AdminPage", "Manage");
                    }
                    else
                    {
                        return RedirectToLocal(returnUrl);
                    }                   
                }
                //  need to edit based on employee role  

                else if (positionID > 0)
                {
                    var empPosID = db.Employees.Where(u => u.AccountID == accID).Select(u => u.PositionID).Single();
                    int pos = Convert.ToInt16(empPosID);
                    var position = db.Positions.Where(u => u.PositionID == pos).Select(u => u.Name).Single();

                    if (position.ToString() == "Manager")
                    {
                        var logindetails = loginInfo.First();
                        string role2 = position.ToString();
                        SignInUser(logindetails.UserName, role2, false);
                        //return RedirectToAction("Index", "Deliveries");
                        if (returnUrl == null)
                        {
                            return RedirectToAction("ManagerPage", "Manage");
                        }
                        else
                        {
                            return RedirectToLocal(returnUrl);
                        }
                    }
                    else if (position.ToString().ToLower() == "human resources")
                    {
                        var logindetails = loginInfo.First();
                        string role2 = position.ToString();
                        SignInUser(logindetails.UserName, role2, false);
                        //return RedirectToAction("Index", "Deliveries");
                        if (returnUrl == null)
                        {
                            return RedirectToAction("HumanResourcePage", "Manage");
                        }
                        else
                        {
                            return RedirectToLocal(returnUrl);
                        }
                    }

                    else if (position.ToString().ToLower() == "clerk")
                    {
                        var logindetails = loginInfo.First();
                        string role2 = position.ToString();
                        SignInUser(logindetails.UserName, role2, false);
                        //return RedirectToAction("Index", "Deliveries");
                        if (returnUrl == null)
                        {
                            return RedirectToAction("ClerkPage", "Manage");
                        }
                        else
                        {
                            return RedirectToLocal(returnUrl);
                        }
                    }

                    else if (position.ToString().ToLower() == "worker")
                    {
                        var logindetails = loginInfo.First();
                        string role2 = position.ToString();
                        SignInUser(logindetails.UserName, role2, false);
                        //return RedirectToAction("Index", "Deliveries");
                        if (returnUrl == null)
                        {
                            return RedirectToAction("WorkerPage", "Manage");
                        }
                        else
                        {
                            return RedirectToLocal(returnUrl);
                        }
                    }

                    else if (position.ToString().ToLower() == "delivery")
                    {
                        var logindetails = loginInfo.First();
                        string role2 = position.ToString();
                        SignInUser(logindetails.UserName, role2, false);
                        //return RedirectToAction("Index", "Deliveries");
                        if (returnUrl == null)
                        {
                            return RedirectToAction("DeliveryPage", "Manage");
                        }
                        else
                        {
                            return RedirectToLocal(returnUrl);
                        }
                    }

                    else if (position.ToString().ToLower() == "foreman")
                    {
                        var logindetails = loginInfo.First();
                        string role2 = position.ToString();
                        SignInUser(logindetails.UserName, role2, false);
                        //return RedirectToAction("Index", "Deliveries");
                        if (returnUrl == null)
                        {
                            return RedirectToAction("ForemanPage", "Manage");
                        }
                        else
                        {
                            return RedirectToLocal(returnUrl);
                        }
                    }
                    else
                    {
                        // Default role = worker role
                        var logindetails = loginInfo.First();
                        string role2 = "Worker";
                        SignInUser(logindetails.UserName, role2, false);
                        //return RedirectToAction("Index", "Deliveries");
                        return RedirectToLocal(returnUrl);

                    }
                }
                //else if (positionID == 0)
                //{
                //    return RedirectToAction("Index", "ShowData");
                //}

                else if (loginInfo.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "Incorrect Username or Password");
                    //ViewBag.LoginError = "Incorrect Username or Password";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    //return View(model);
                }
            }
            catch
            {
                //ViewBag.errorLogin = ex.ToString();
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
            }
            return View(model);
        }


        //customer login method... is called by register method
        [AllowAnonymous]
        public ActionResult custLogin(LoginModel model)
        {
            var loginInfo = (from c in db.Accounts
                             where c.UserName == model.Username
                             where c.Password == model.Password
                             select new { c.UserName, c.Password }).ToList();
            var accType = (from c in db.Accounts
                           where c.UserName == model.Username
                           where c.Password == model.Password
                           select c.Type).Single();

            if (accType == "Customer" || accType == "customer")
            {
                var Role = "Customer";
                var Custlogindetails = loginInfo.First();

                SignInUser(Custlogindetails.UserName, Role, false);
                //return this.RedirectToLocal(returnUrl);
            }
            return View(model);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult ChangePassword()
        {
                return View();
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(PasswordViewModel passModel)
        {
            string name = User.Identity.Name;

            var getAcctId = (from x in db.Accounts
                             where x.UserName == name
                             select x.AccountID).First();

            int accid = Convert.ToInt16(getAcctId);
            int? id = accid;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(accid);
            if (account != null)
            {
                account.Password = passModel.Password;
                account.ConfirmPassword = passModel.ConfirmPassword;
                account.Role = null;
                account.Customer = null;
                account.Employee = null;

                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.passMessage = "Password updated successfully";
                return RedirectToAction("CustomerPage", "Manage");
            }
            else
            {
                return View(account);
                //return HttpNotFound();
            }
            
        }

        //-----------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // Log out
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {

                string name = User.Identity.Name;

                var getAcctId = (from x in db.Accounts
                                 where x.UserName == name
                                 select x.AccountID).First();

                int accid = Convert.ToInt16(getAcctId);

                var getCustId = (from x in db.Customers
                                 where x.AccountID == accid
                                 select x.CustomerID).First();

                int customerID = Convert.ToInt16(getCustId);


                var mergedList = (from c in db.Customers
                                  join x in db.Orders
                                  on c.CustomerID equals x.CustomerID
                                  join y in db.StockOrders
                                  on x.OrderID equals y.OrderID
                                  join z in db.Orders
                                  on y.OrderID equals z.OrderID
                                  where z.CustomerID == customerID
                                  orderby c.Name ascending
                                  select new
                                  {
                                      y.SOID
                                  }).ToList();



            if (mergedList.Count()!=0)
            {
                int soId = Convert.ToInt16(mergedList.First().SOID);

                var stockoid = db.StockOrders.Where(x => x.SOID == soId).Select(y => y.SOID).First();
                int stockorderid = Convert.ToInt16(stockoid);


                var findOrderId = (from c in db.Customers
                                   join x in db.Orders
                                   on c.CustomerID equals x.CustomerID
                                   where x.CustomerID == customerID
                                   where x.status == "Pending"
                                   orderby x.OrderID descending
                                   select new
                                   {
                                       x.OrderID
                                   }).First();

                int orderId = Convert.ToInt16(findOrderId.OrderID);

                StockOrder stockorders = db.StockOrders.Find(stockorderid);
                db.StockOrders.Remove(stockorders);
                db.SaveChanges();


                Order orders = db.Orders.Find(orderId);
                db.Orders.Remove(orders);
                db.SaveChanges();

                var request = Request.GetOwinContext();
                var authManager = request.Authentication;
                authManager.SignOut();
            }
                // add empty cart code here
            }
            catch
            {
                var request = Request.GetOwinContext();
                var authManager = request.Authentication;
                authManager.SignOut();

                return RedirectToAction("Store", "Stocks");
            }
            return RedirectToAction("Login", "Accounts");
        }

        // Sign In.    
        private void SignInUser(string username, string Role, bool isPersistent)
        {
            var claims = new List<Claim>();
            try
            {
                claims.Add(new Claim(ClaimTypes.Name, username));
                claims.Add(new Claim(ClaimTypes.Role, Role));
                var claimIds = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "ShowData");
        }

        [AllowAnonymous]
        public ActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            try
            {
                if (Request.IsAuthenticated)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Content("Access Denied");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return View();
        }
    }
}
