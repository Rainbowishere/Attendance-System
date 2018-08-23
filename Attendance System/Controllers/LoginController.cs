using Attendance_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance_System.Controllers
{
    public class LoginController : Controller
    {
        private AttendanceDbContext db = new AttendanceDbContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult LoadEmployee(string id)
        {
            var existingUser = db.People.Find(id);

            if (existingUser == null)
            {
                return PartialView("Register");
            }
            else
            {
                return PartialView(existingUser);
            }

            //var person = db.People.Find(id);
            
        }
    }
}