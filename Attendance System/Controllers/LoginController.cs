using Attendance_System.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult LoadEmployee(string id)
        {
            var existingUser = db.People.Find(id);
            var isCheckedIn = db.CheckinCheckouts.Where(x => x.PhoneNumberID == id && x.Checkin != null && x.Checkout == null).Select(x => x.ID).FirstOrDefault();

            if (existingUser == null)
            {
                return PartialView("Register");
            }
            else
            {
                if (isCheckedIn == default(Guid))
                {
                    return PartialView(existingUser);
                }
                else
                {
                    return PartialView("Checkout", isCheckedIn);
                }

            }
        }

        // POST: CheckinCheckouts1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckIn([Bind(Include = "ID,PhoneNumberID,Checkin,Checkout,Purpose,Device,Purpose2")] CheckinCheckout checkinCheckout,string Purpose2)
        {
            if (ModelState.IsValid)
            {
                if (checkinCheckout.Purpose == "ອື່ນໆ...")
                {
                    checkinCheckout.Purpose = Purpose2;
                }

                checkinCheckout.ID = Guid.NewGuid();
                checkinCheckout.Checkin = DateTimeOffset.Now;
                db.CheckinCheckouts.Add(checkinCheckout);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            return View("index");
        }


        // POST: People1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "PhoneNumberID,FullName,Source,EmployeeID,Device")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(person);
        }
    }
}