using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Factory;
using Web.Factory.FactoryMethod;
using Web.Manages;
using Web.Models;
using EmployeeManagerFactory = Web.Factory.FactoryMethod.EmployeeManagerFactory;
using Web.Factory.AbstractFactory.AbstractInterface;
using Web.Factory.AbstractFactory.ConcreteFactory;
using Web.Factory.AbstractFactory.Client;
using Web.WithoutBuilderDP;
using Web.BuilderDesignPattern.IBuilder;
using Web.BuilderDesignPattern.Director;
using Web.BuilderDesignPattern.ConcreteBuilder;

namespace Web.Controllers
{
    public class EmployeesController : BaseController
    {
        private EmployeePortalEntities db = new EmployeePortalEntities();

        //----------------------------- For Without Build Design Pattern---------------------
        //[HttpGet]
        //public ActionResult BuildSystem(int? employeeID)
        //{
        //    return View(employeeID);
        //}

        // For Without Builder Design Pattern
        //[HttpPost]
        //public ActionResult BuildSystem(int employeeID, string RAM, string HDDrive)
        //{
        //    Employee employee = db.Employees.Find(employeeID);
        //    ComputerSystem computerSystem = new ComputerSystem(RAM, HDDrive);
        //    employee.SystemConfigurationDetails = computerSystem.Build();
        //    db.Entry(employee).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //----------END------------------- For Without Build Design Pattern---------------------

        //-------------------------------For With Builder Design Pattern------------------------------
        [HttpGet] 
        public ActionResult BuildSystem(int? employeeID)
        {
            Employee employee = db.Employees.Find(employeeID);
            if(!string.IsNullOrEmpty(employee.SystemConfigurationDetails))
            {
                ViewBag.Error = "Details already filled";
                var employees = db.Employees.Include(e => e.Employee_Type);
                return View("Index", employees.ToList());
            }
            else
            {
                if (employee.ComputerDetails.Contains("Laptop"))
                    return View("BuildLaptop", employeeID);
                else
                    return View("BuildDesktop", employeeID);
            }
        }

        [HttpPost]
        public ActionResult BuildLaptop(FormCollection formCollection)
        {
            Employee employee =
                  db.Employees.Find(Convert.ToInt32(formCollection["employeeID"]));
            //Concrete Builder
            ISystemBuilder systemBuilder = new LaptopBuilder();
            //Director
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.BuildSystem(systemBuilder, formCollection);
            Web.BuilderDesignPattern.Product.ComputerSystem system = systemBuilder.GetSystem();

            employee.SystemConfigurationDetails =
        string.Format("RAM : {0}, HDDSize : {1}, TouchScreen: {2}"
        , system.RAM, system.HDDSize, system.TouchScreen);

            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult BuildDesktop(FormCollection formCollection)
        {
            //Step 1
            Employee employee
        = db.Employees.Find(Convert.ToInt32(formCollection["employeeID"]));
            //Step 2 Concrete Builder
            ISystemBuilder systemBuilder = new DesktopBuilder();
            //Step 3 Director
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.BuildSystem(systemBuilder, formCollection);
            //Step 4 return the system
            Web.BuilderDesignPattern.Product.ComputerSystem system = systemBuilder.GetSystem();

            employee.SystemConfigurationDetails =
        string.Format("RAM : {0}, HDDSize : {1}, Keyboard: {2}, Mouse : {3}"
        , system.RAM, system.HDDSize, system.KeyBoard, system.Mouse);
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //-------------END------------------For With Builder Design Pattern------------------------------

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Employee_Type);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
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

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeTypeID = new SelectList(db.Employee_Type, "Id", "EmployeeType");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,JobDescription,Number,Department,HourlyPay,Bonus,EmployeeTypeID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                //-------------- Below was without Factory-----------------------
                //if(employee.EmployeeTypeID==1)
                //{
                //    employee.HourlyPay = 8;
                //    employee.Bonus = 10;
                //}
                //else if(employee.EmployeeTypeID==2)
                //{
                //    employee.HourlyPay = 12;
                //    employee.Bonus = 5;
                //}

                //-------------- Below was with Simple Factory-----------------------

                //EmployeeManagerFactory empFactory = new EmployeeManagerFactory();
                //IEmployeeManager empManager = empFactory.GetEmployeeManager(employee.EmployeeTypeID);

                //employee.Bonus = empManager.GetBonus();
                //employee.HourlyPay = empManager.GetPay();

                //-------------- Below was with Complex Factory-----------------------

                //BaseEmployeeFactory empFactory =new EmployeeManagerFactory().CreateFactory(employee);

                EmployeeManagerFactory empManagerFactory = new EmployeeManagerFactory();
                BaseEmployeeFactory empFactory = empManagerFactory.CreateFactory(employee);
                empFactory.ApplySalary();

                EmployeeSystemFactory empsystemfactory = new EmployeeSystemFactory();
                IComputerFactory factory = empsystemfactory.Create(employee);
                EmployeeSystemManager manager = new EmployeeSystemManager(factory);
                employee.ComputerDetails = manager.GetSystemDetails();

                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeTypeID = new SelectList(db.Employee_Type, "Id", "EmployeeType", employee.EmployeeTypeID);
            return View(employee);
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
            ViewBag.EmployeeTypeID = new SelectList(db.Employee_Type, "Id", "EmployeeType", employee.EmployeeTypeID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,JobDescription,Number,Department,HourlyPay,Bonus,EmployeeTypeID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeTypeID = new SelectList(db.Employee_Type, "Id", "EmployeeType", employee.EmployeeTypeID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
