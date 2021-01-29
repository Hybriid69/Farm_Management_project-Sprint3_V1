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

namespace Sprint_3_V1.Controllers
{
    public class CustomersController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        [Authorize(Roles = "Admin , Manager")]
        public ActionResult Search(Customer customer)
        {
            return PartialView(customer);
        }
        [Authorize(Roles = "Admin , Manager , Clerk")]
        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Account);
            return View(customers.ToList());
        }

        [Authorize(Roles = "Admin , Manager , Customer , Clerk")]
        public ActionResult Cart(string name)
        {
            if (name == "" || name == null)
            {
                name = User.Identity.Name;
            }           

            var getAcctId = (from x in db.Accounts
                             where x.UserName == name
                             select x.AccountID).First();

            int accid = Convert.ToInt16(getAcctId);

            var getCustId = (from x in db.Customers
                             where x.AccountID == accid
                             select x.CustomerID).First();

            int customerID = Convert.ToInt16(getCustId);

            var stockCart = (from x in db.StockOrders
                             join y in db.Orders
                             on x.OrderID equals y.OrderID
                             where y.CustomerID == customerID
                             select x).ToList();
            if (stockCart.Count == 0)
            {
                ViewBag.cartempty = "No Items in Cart";
            }
            return View(stockCart);
        }

        [Authorize(Roles = "Admin , Manager , Customer , Clerk")]
        public ActionResult ClearCart()
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

            var stockCart = (from x in db.StockOrders
                             join y in db.Orders
                             on x.OrderID equals y.OrderID
                             where y.CustomerID == customerID
                             select x.SOID).ToList();
            foreach (var item in stockCart)
            {
                int id = Convert.ToInt16(item);
                StockOrder stockRemove = db.StockOrders.Find(id);
                db.StockOrders.Remove(stockRemove);
                db.SaveChanges();
            }
            return RedirectToAction("Cart", "Customers");
        }

        [Authorize(Roles = "Admin , Manager , Customer , Clerk")]
        public ActionResult RemoveFromCart(int? id, StockOrder stockorder)
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

            //var stockCart = (from x in db.StockOrders
            //                 where x.StockID == id
            //                 select x).ToList();

            stockorder = db.StockOrders.Find(id);

            var stockQuantity = db.StockOrders.Where(x=>x.SOID == id).Select(y=>y.Quantity).First();
            double qty = Convert.ToDouble(stockQuantity);

            if (qty>1)
            {
                double newQty = qty - 1;
                //stockorder.SOID = Convert.ToInt16(id);
                stockorder.Quantity = newQty;
                db.Entry(stockorder).State = EntityState.Modified;
                db.SaveChanges();
            }
            else if (qty == 0 || qty ==1)
            {
                StockOrder stockRemove = db.StockOrders.Find(id);
                db.StockOrders.Remove(stockRemove);
                db.SaveChanges();
            }
            return RedirectToAction("Cart","Customers");
        }

        [Authorize(Roles = "Admin , Manager , Customer , Clerk")]
        public ActionResult RemoveAll(int? id, StockOrder stockorder)
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

            stockorder = db.StockOrders.Find(id);

            var stockQuantity = db.StockOrders.Where(x => x.SOID == id).Select(y => y.Quantity).First();
            double qty = Convert.ToDouble(stockQuantity);

            if (qty >= 0)
            { 
                StockOrder stockRemove = db.StockOrders.Find(id);
                db.StockOrders.Remove(stockRemove);
                db.SaveChanges();
                ViewBag.empty = "Cart empty";
            }
            return RedirectToAction("Cart", "Customers");
        }

        //-----------------------------------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin , Manager , Clerk")]
        [HttpPost]
        public ActionResult Index(string search)
        {
            // allows the admin to search for a customer

            if (search != null)
            {
                List<Customer> searchcust = new List<Customer>();
                var info = (from x in db.Customers
                            where x.Name.Contains(search) || x.CustomerID.ToString().Contains(search)
                            select x).ToList();
                if (info.Count() > 0)
                {
                    foreach (var item in info)
                    {
                        Customer custFound = new Customer();
                        custFound.Account = new Account();
                        custFound.Account.UserName = item.Account.UserName;
                        custFound.CustomerID = item.CustomerID;
                        custFound.Disabled = item.Disabled; custFound.Email = item.Email;
                        custFound.Multi = item.Multi;
                        custFound.Name = item.Name;
                        custFound.Number = item.Number;
                        custFound.Type = item.Type;
                        custFound.Address = item.Address;
                        custFound.AccountID = item.AccountID;
                        searchcust.Add(custFound);
                    }
                    return View(searchcust);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        //-----------------------------------------------------------------------------------------------------------------

        // GET: Customers/Details/5
        [Authorize(Roles = "Admin , Manager , Clerk")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        //[Authorize(Roles = "Admin , Manager")]
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "UserName");
            return View();
        }

        // POST: CustomerReg/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Customer")]
        public ActionResult CustomerDetails()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerDetails([Bind(Include = "CustomerID,Name,CustType,Multi,Number,Email,Address")] CustomerViewModel customerViewModel, Customer customer)
        {
            // returns this view after customer passes regiter view in accounts
            string reciever = "";
            string customername = "";
            int Accid = 0;
            customerViewModel.CustType = "Customer";

            customer.Disabled = false;

            if (TempData.ContainsKey("Username"))
            {
                string name = TempData["Username"].ToString();
                var user = db.Accounts.Where(x => x.UserName == name).Select(y => y.AccountID).First();
                Accid = Convert.ToInt32(user);

            }

            if (ModelState.IsValid)
            {
                reciever = customerViewModel.Email;
                customername = customerViewModel.Name;
                // inserts customer info to customer table using corresponding account ID 

                customer.CustomerID = customerViewModel.CustomerID;
                customer.AccountID = Accid;
                customer.Name = customerViewModel.Name;
                customer.Type = customerViewModel.CustType;
                customer.Multi = customerViewModel.Multi;
                customer.Number = customerViewModel.Number;
                customer.Address = customerViewModel.Address;

                db.Customers.Add(customer);
                db.SaveChanges();

                AccountsController acc = new AccountsController();
                var subject = "Farmer's Fresh Produce Registration";
                var body = "Welcome to Farmer's Fresh Produce " + customername + "\r\n Please enjoy our services";
                acc.Confirmation(reciever, customername,subject,body);

                return RedirectToAction("Store","Stocks");
            }
            return View(customerViewModel);
        }


        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin , Manager , Clerk")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "UserName", customer.AccountID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin , Manager , Clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,Name,Type,Multi,Number,Email,Address,Disabled,AccountID")] Customer customer, int? id)
        {
            var find = (from x in db.Customers
                        where x.CustomerID == id
                        select x.AccountID).Single();

            customer.AccountID = Convert.ToInt16(find);
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "UserName", customer.AccountID);
            return View(customer);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
