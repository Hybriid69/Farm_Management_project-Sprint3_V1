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
    public class OrdersController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        [Authorize(Roles = "Admin , Manager , Customer , Clerk, Foreman")]
        public ActionResult AddToOrders([Bind(Include = "OrderID,status,CustomerID,Total")] Order order, StockViewModel svm)
        {
            //try
            //{
            DateTime today = DateTime.Now.Date;
            int stockid = Convert.ToInt16(svm.StockID);
            int quantity = Convert.ToInt16(svm.QuantityWanted);
            var cropvar = db.Stocks.Where(x => x.StockID == stockid).Select(y => y.CropID).First();
            int cropid = Convert.ToInt16(cropvar);
            var cropname = db.CropInfoes.Where(x => x.CropID == cropid).Select(y => y.Name).First();
            var price = db.Stocks.Where(a => a.StockID == stockid).Select(b => b.Price).Single();

            bool check = svm.lowQuantitycheck(quantity);
            if (check == true)
            {
                string name =User.Identity.Name;

                var getAcctId = (from x in db.Accounts
                                 where x.UserName == name
                                 select x.AccountID).First();

                int accid= Convert.ToInt16(getAcctId);

                var getCustId = (from x in db.Customers
                                 where x.AccountID == accid
                                 select x.CustomerID).First();

                int customerID = Convert.ToInt16(getCustId);

                order.CustomerID = customerID;

                var findCust = db.Customers.Where(a => a.CustomerID == order.CustomerID).Select(b => b.CustomerID).Count();

                var checkCustOrders = db.Orders.Where(a => a.CustomerID == order.CustomerID).Select(b => b.OrderID).Count();

                var oDate = db.Orders.Where(a => a.CustomerID == order.CustomerID).OrderByDescending(x => x.OrderID).Select(b => b.DateOfOrder).Count();

               // DateTime orderDate = Convert.ToDateTime(oDate);

                order.DateOfOrder = today;

                if (checkCustOrders > 0 && oDate > 0 )
                {
                    var oId = db.Orders.Where(a => a.CustomerID == order.CustomerID).OrderByDescending(x => x.OrderID).Select(b => b.OrderID).First();

                    var total = db.Orders.Where(a => a.CustomerID == order.CustomerID).OrderByDescending(x => x.OrderID).Select(b => b.Total).First();
                    order.OrderID = Convert.ToInt16(oId);

                    order.Total = (Convert.ToDouble(price) * quantity) + Convert.ToDouble(total);
                    order.status = "Pending";

                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();

                }
                else
                {
                    order.Total = (Convert.ToDouble(price))*quantity;
                    order.status = "Pending";

                    db.Orders.Add(order);
                    db.SaveChanges();
                }

                int stockID = Convert.ToInt16(stockid);

                var subPrice = db.Stocks.Where(x => x.StockID == stockID).Select(s => s.Price).Single();

                StockOrder so = new StockOrder();
                so.StockID = stockID;
                so.OrderID = order.OrderID;
                so.Quantity = quantity;
                so.SubPrice = Convert.ToInt16(subPrice);

                StockOrdersController soc = new StockOrdersController();
                soc.AddStockOrders(so);

                Stock st = new Stock();
                st.stockDeduct(so.Quantity, stockid);

                TempData["SuccessMessage"] = quantity + " " + cropname + " successfully added to orders";

                return RedirectToAction("Store", "Stocks");
            }
            else
            {
                TempData["lessQuantityMessage"] = "Selected Quantity not available";
                return RedirectToAction("Store", "Stocks");
            }
            //}
            //catch
            //{
            //    return Content("Failed");
            //}           
        }

        [Authorize(Roles = "Admin , Manager , Customer , Clerk, Foreman")]
        public ActionResult CheckOut()
        {
            CheckoutViewModel checkout = new CheckoutViewModel();
            DateTime today = DateTime.Now.Date;
            string name = User.Identity.Name;

            var getAcctId = (from x in db.Accounts
                             where x.UserName == name
                             select x.AccountID).First();

            int accid = Convert.ToInt16(getAcctId);

            var getCustId = (from x in db.Customers
                             where x.AccountID == accid
                             select x.CustomerID).First();

            int customerID = Convert.ToInt16(getCustId);

            var getCustName = (from x in db.Customers
                             where x.CustomerID == customerID
                             select x.Name).First();
            try
            {

            
            var getOrderTotal = (from x in db.Orders
                               where x.CustomerID == customerID
                               where x.DateOfOrder == today
                               where x.status== "Pending"
                                 orderby x.OrderID descending
                               select x.Total).First();
            double OrderTotal = Convert.ToDouble(getOrderTotal);

            var orders = db.Orders.Where(a => a.CustomerID == customerID).OrderBy(x => x.Customer.Name).ToList();

            checkout.CustomerID = customerID;
            checkout.CustomerName = getCustName.ToString();
            //checkout.PaymentOption = payment;
            //checkout.CollectionOption = collection;
            checkout.Total = OrderTotal;
                return View(checkout);

            }
            catch
            {
                ViewBag.ordererror = "No current pending orders";
                return View();
            }
            
        }
        static int statOrderId;

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin , Manager , Customer , Clerk, Foreman")]
        public ActionResult CheckOut(CheckoutViewModel checkout)
        {
            DateTime today = DateTime.Now.Date;
            string name = User.Identity.Name;

            var getAcctId = (from x in db.Accounts
                             where x.UserName == name
                             select x.AccountID).First();

            int accid = Convert.ToInt16(getAcctId);

            var getCustId = (from x in db.Customers
                             where x.AccountID == accid
                             select x.CustomerID).First();

            int customerID = Convert.ToInt16(getCustId);

            var email = (from x in db.Customers
                         where x.CustomerID == customerID
                         select x.Email).First();
            string custEmail = email.ToString();

            var getCustName = (from x in db.Customers
                               where x.CustomerID == customerID
                               select x.Name).First();
            string custname = getCustName.ToString();
            var getOrderid = (from x in db.Orders
                                 where x.CustomerID == customerID
                                 select x.OrderID).First();
            int OrderId = Convert.ToInt16(getOrderid);

            //var orders = db.Orders.Where(a => a.CustomerID == customerID).OrderBy(x => x.Customer.Name).ToList();

            checkout.CustomerID = customerID;
            checkout.CustomerName = custname;

            Order order = db.Orders.Find(OrderId);
            order.status = "Processing";
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();

            int customerId = Convert.ToInt16(order.CustomerID);
            int orderId = Convert.ToInt16(order.OrderID);
            statOrderId= Convert.ToInt16(order.OrderID);
            int total = Convert.ToInt16(order.Total);

            Sale sale = new Sale();
            sale.CustomerID = customerId;
            sale.Date = DateTime.Now.Date;
            sale.Total = total;
            sale.OrderID = orderId;

            SalesController sc = new SalesController();
            sc.AddSale(sale);
            
            string subject="Order"+ orderId + "Confirmation";
            string body = custname + " your order= "+orderId+" of "+total+" has be successfully placed." ;

            AccountsController acc = new AccountsController();
            acc.Confirmation(custEmail, custname, subject, body);


            var stockoid = db.StockOrders.Where(x => x.OrderID == orderId).Select(y => y.SOID).First();
            int stockorderid = Convert.ToInt16(stockoid);

            StockOrder stockorders= db.StockOrders.Find(stockorderid);
            db.StockOrders.Remove(stockorders);
            db.SaveChanges();

            return RedirectToAction("OrderHistory","Orders");
        }


        [Authorize(Roles = "Admin , Manager , Customer , Clerk, Foreman")]
        public ActionResult CheckOutHelper(CheckoutItemsViewModel checkoutitems)
        {
            DateTime today = DateTime.Now.Date;

            string name = User.Identity.Name;

            var getAcctId = (from x in db.Accounts
                             where x.UserName == name
                             select x.AccountID).First();

            int accid = Convert.ToInt16(getAcctId);

            var getCustId = (from x in db.Customers
                             where x.AccountID == accid
                             select x.CustomerID).First();

            int customerID = Convert.ToInt16(getCustId);

            List<CheckoutItemsViewModel> civm = new List<CheckoutItemsViewModel>();


            var mergedList = (from c in db.CropInfoes
                              join x in db.Stocks
                              on c.CropID equals x.CropID
                              join y in db.StockOrders
                              on x.StockID equals y.StockID
                              join z in db.Orders
                              on y.OrderID equals z.OrderID
                              where z.CustomerID == customerID
                              where z.DateOfOrder == today
                              orderby c.Name ascending
                              select new
                              {
                                  c.Name,
                                  y.Quantity
                              }).ToList();
            if (mergedList.Count() > 0)
            {
                foreach (var item in mergedList)
                {
                    CheckoutItemsViewModel civ = new CheckoutItemsViewModel();
                    civ.StockName = item.Name;
                    civ.Quantity = item.Quantity;
                    civm.Add(civ);
                }
            }
            return PartialView(civm);
        }

        [Authorize(Roles = "Admin , Manager , Customer , Clerk, Foreman")]
        public ActionResult OrderHistory()
        {
            //TODO : orderhistory

            string name = User.Identity.Name;

            var getAcctId = (from x in db.Accounts
                             where x.UserName == name
                             select x.AccountID).First();

            int accid = Convert.ToInt16(getAcctId);

            var getCustId = (from x in db.Customers
                             where x.AccountID == accid
                             select x.CustomerID).First();

            int customerID = Convert.ToInt16(getCustId);

            var orders = db.Orders.Where(a => a.CustomerID == customerID).OrderBy(x => x.Customer.Name).ToList();
            return View(orders.ToList());
        }

        [Authorize(Roles = "Admin , Manager , Customer , Clerk, Foreman")]
        public ActionResult OrderHistoryAll()
        {
            var orders = db.Orders.Include(o => o.Customer);
            return View(orders.ToList());
        }

        [Authorize(Roles = "Admin , Manager , Clerk , Foreman , Delivery")]
        public ActionResult PendingOrders()
        {
            var orders = db.Orders.Where(a => a.status == "Pending" || a.status == "pending").ToList();
            return View(orders.ToList());
        }

        // GET: Orders
        [Authorize(Roles = "Admin , Manager , Clerk, Foreman")]
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "Admin , Manager , Clerk, Foreman")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles = "Admin , Manager , Clerk, Foreman")]
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin , Manager , Clerk, Foreman")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,status,CustomerID,Total")] Order order)
        {
            //order.status = "Pending";
            //order.CustomerID = 1;
            //order.Total = 10;
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", order.CustomerID);
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin , Manager , Clerk, Foreman")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", order.CustomerID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin , Manager , Clerk, Foreman")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,status,CustomerID,Total")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

                if (order.status.ToString().ToLower() == "completed")
                {
                    /////// Create Sale Entry
                    int customerId = Convert.ToInt16(order.CustomerID);
                    int orderId = Convert.ToInt16(order.OrderID);
                    int total = Convert.ToInt16(order.Total);

                    Sale sale = new Sale();
                    sale.CustomerID = customerId;
                    sale.Date = DateTime.Now.Date;
                    sale.Total = total;
                    sale.OrderID = orderId;

                    SalesController sc = new SalesController();
                    sc.AddSale(sale);

                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("Failed to create sale");
                }

            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", order.CustomerID);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin , Manager , Clerk")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [Authorize(Roles = "Admin , Manager , Clerk")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
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
