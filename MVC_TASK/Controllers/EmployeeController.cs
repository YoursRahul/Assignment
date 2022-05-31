using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_TASK.Models;
using System.Data.Entity;

namespace MVC_TASK.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeePortalEntities db;
        public EmployeeController()
        {
            db=new EmployeePortalEntities();
        }

        public ActionResult Log()
        {
            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult AdminLogin(FormCollection from)
        {
            string u = from["uname"];
            string p = from["pass"];

            if (u == "Admin" && p == "Admin")
            {
                Session["user"] = u;
                Session.Timeout = 1;
                return RedirectToAction("AdminDashboard");
            }
            else
            {
                ViewBag.msg = "Invoid User Name and Password";
                return View();
            }

        }

        public ActionResult AdminDashboard()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("AdminLogin");
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(EmployeeModel e)
        {
            if (e.User_Password != e.Confirm_Password)
            {
                ViewBag.msg = "Password and Confirm password are not matched";
                return View();
            }
            else
            {
                tblEmployee emp = new tblEmployee() { First_Name = e.First_Name, Last_Name = e.Last_Name, User_Password = e.User_Password, Email_Address = e.Email_Address, Birth_Date = e.Birth_Date };
                db.tblEmployees.Add(emp);
                db.SaveChanges();

                return RedirectToAction("Success");
            }
        }


        public ActionResult Success()
        {

            return View();

        }


        public ActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Login(tblEmployee e)
        {
            tblEmployee emp = db.tblEmployees.ToList().FirstOrDefault(r => r.Email_Address.Equals(e.Email_Address) & r.User_Password.Equals(e.User_Password));
            if(emp==null)
            {
                ViewBag.msg = "Invalid Email Address or Password";
                return View();
            }
          
            else
            {
                Session["employee_name"] = emp.First_Name + " " + emp.Last_Name;
                Session["employee_id"] = emp.Employee_ID;
                return RedirectToAction("Dashboard");
            }

        }


        public ActionResult Dashboard()
        {
            if (Session["employee_id"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                int id = Convert.ToInt32(Session["employee_id"].ToString());
                tblEmployee e = db.tblEmployees.Find(id);
                return View(e);
            }
        }


        public ActionResult Logout()
        {
            Session["employee_id"] = null;
            Session["employee_name"] = null;
            return RedirectToAction("Login");
        }

        public JsonResult GetEmployee()
        {
            return Json(db.tblEmployees.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEmployeeByID(int id)
        {
            tblEmployee v = db.tblEmployees.ToList().FirstOrDefault(e => e.Employee_ID.Equals(id));
            return Json(v, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Adminlogout()
        {
            Session["User"] = null;
            return RedirectToAction("AdminLogin");
        }
        public ActionResult Edit(int id)
        {
            tblEmployee st = db.tblEmployees.Find(id);
            return View(st);
        }
        [HttpPost]
        public ActionResult Edit(tblEmployee st)
        {
            db.Entry<tblEmployee>(st).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AdminDashboard");
        }
        public ActionResult Delete(int id)
        {
            tblEmployee st = db.tblEmployees.Find(id);
            db.tblEmployees.Remove(st);
            db.SaveChanges();
            return RedirectToAction("AdminDashboard");
        }
	}
}