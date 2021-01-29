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
    [Authorize(Roles = "Admin , Manager , Human resources")]

    public class EmployeesController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        public ActionResult EmployeeAccount(Employee employee)
        {
            return PartialView();
        }
        // GET: Employees

        public ActionResult Search(Employee employee)
        {
            return PartialView(employee);
        }
        // GET: Employees

        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Account).Include(e => e.Position); //.Include(e => e.Farm);
            return View(employees.ToList());
        }
        [HttpPost]

        public ActionResult Index(string search)
        {
            // search for an employee
            if (search != null)
            {
                List<Employee> searchemp = new List<Employee>();
                var info = (from x in db.Employees
                            where x.Name.Contains(search) || x.EmployeeID.ToString().Contains(search)
                            select x).ToList();
                if (info.Count() > 0)
                {
                    foreach (var item in info)
                    {
                        Employee empFound = new Employee();
                        empFound.Account = new Account();
                        empFound.EmployeeID = item.EmployeeID;
                        empFound.Email = item.Email;
                        empFound.EmpPos = item.EmpPos;
                        empFound.Name = item.Name;
                        empFound.Surname = item.Surname;
                        empFound.AccountID = item.AccountID;
                        empFound.KinContactNum = item.KinContactNum;
                        empFound.IDNumber = item.IDNumber;
                        empFound.ContactNum = item.ContactNum;
                        empFound.DateHired = item.DateHired;
                        empFound.Position = item.Position;
                        empFound.Account.UserName = item.Account.UserName;
                        searchemp.Add(empFound);
                    }
                    return View(searchemp);
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

        // GET: Employees/Details/5
        [Authorize(Roles = "Admin , Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }


        // GET: EmployeeAccounts/Create/ to be added by admin

        public ActionResult AddEmployee()
        {
            ViewBag.FarmID = new SelectList(db.Farms, "FarmID", "FarmName");
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Name");
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GName");
            return View();
        }

        // POST: EmployeeAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee([Bind(Include = "EmployeeID,Name,Surname,IDNumber,DateHired,ContactNum,KinContactNum,Email,FarmID,PositionID,GroupID,UserName,Password,ConfirmPassword,Type")] EmployeeAccountsViewModel employeeAccounts)
        {
            // employeeAccounts.Type = "Employee";
            if (ModelState.IsValid)
            {
                try
                {
                    // add employee account to account table
                    var varBefore = db.Accounts.Select(x => x.AccountID).Count();
                    int before = Convert.ToInt16(varBefore);
                    Account account = new Account();
                    account.UserName = employeeAccounts.UserName;
                    account.Password = employeeAccounts.Password;
                    account.ConfirmPassword = employeeAccounts.ConfirmPassword;
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    //Acc ID
                    var accFind = db.Accounts.Where(x => x.UserName == account.UserName).Select(y => y.AccountID).Single();
                    int accId = Convert.ToInt16(accFind);

                    // add employee details to employee table
                    var varAfter = db.Accounts.Select(x => x.AccountID).Count();
                    int after = Convert.ToInt16(varAfter);
                    var beforeEmployeeIsAdded = db.Employees.Select(x => x.EmployeeID).Count();

                    if (before < after)
                    {
                        int positionId = employeeAccounts.PositionID;
                        int farmId = employeeAccounts.FarmID;
                        int groupId = employeeAccounts.GroupID;
                        Employee employees = new Employee();
                        //employees.Account = new Account();
                        //employees.Position = new Position();
                        employees.EmployeeID = employeeAccounts.EmployeeID;
                        employees.Email = employeeAccounts.Email;
                        employees.Name = employeeAccounts.Name;
                        employees.Surname = employeeAccounts.Surname;
                        employees.AccountID = accId;
                        employees.KinContactNum = employeeAccounts.KinContactNum;
                        employees.IDNumber = employeeAccounts.IDNumber;
                        employees.ContactNum = employeeAccounts.ContactNum;
                        employees.DateHired = employeeAccounts.DateHired;
                        DateTime hired = employeeAccounts.DateHired;
                        employees.PositionID = positionId;
                        employees.FarmID = farmId;
                        employees.GroupID = groupId;
                        // employees.Account.UserName = employeeAccounts.UserName;
                        db.Employees.Add(employees);
                        db.SaveChanges();

                        var checkEmp = db.Employees.Select(x => x.EmployeeID).Count();
                        if (beforeEmployeeIsAdded < checkEmp)
                        {
                            EmpPos employeePosition = new EmpPos();
                            employeePosition.EmployeeID = employees.EmployeeID;
                            employeePosition.Started = hired;
                            employeePosition.PositionID = positionId;
                            employeePosition.Ended = null;
                            db.EmpPos.Add(employeePosition);
                            db.SaveChanges();

                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // undo account commit if employee transaction fails
                    var acc = db.Accounts.Where(x => x.UserName == employeeAccounts.UserName).Select(y => y.AccountID).Single();
                    int ac = Convert.ToInt16(acc);
                    AccountsController emp = new AccountsController();
                    emp.DeleteConfirmed(ac);

                    ViewBag.FarmID = new SelectList(db.Farms, "FarmID", "FarmName", employeeAccounts.FarmID);
                    ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Name", employeeAccounts.PositionID);
                    ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GName", employeeAccounts.GroupID);

                    ViewBag.fatalError = ex.ToString();
                    return View(employeeAccounts);

                }

            }
            ViewBag.FarmID = new SelectList(db.Farms, "FarmID", "FarmName", employeeAccounts.FarmID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Name", employeeAccounts.PositionID);
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GName", employeeAccounts.GroupID);

            return View(employeeAccounts);
        }


        // GET: Employees/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "UserName", employee.AccountID);
            ViewBag.FarmID = new SelectList(db.Farms, "FarmID", "FarmName", employee.FarmID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Name", employee.PositionID);
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GName", employee.GroupID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,Name,Surname,IDNumber,DateHired,ContactNum,KinContactNum,Email,FarmID,PositionID,AccountID,GroupID")] Employee employee, int? id)
        {
            var find = (from x in db.Employees
                        where x.EmployeeID == id
                        select x.AccountID).Single();
            employee.AccountID = Convert.ToInt16(find);

            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "UserName", employee.AccountID);
            ViewBag.FarmID = new SelectList(db.Farms, "FarmID", "FarmName", employee.FarmID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Name", employee.PositionID);
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GName", employee.GroupID);
            return View(employee);
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
