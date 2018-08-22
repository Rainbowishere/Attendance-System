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
        private LTCAttendanceDbContext db = new LTCAttendanceDbContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult LoadEmployee(string id)
        {
            //var person = db.People.Find(id);
            return PartialView(db.People.Find(id));
        }
    }
}