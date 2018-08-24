using Attendance_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // POST: CheckinCheckouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoadEmployee(string id, [Bind(Include = "ID,PhoneNumberID,Checkin,Checkout,Purpose,Device")] CheckinCheckout checkinCheckout)
        {
            //if (ModelState.IsValid)
            //{
            //    checkinCheckout.ID = Guid.NewGuid();
            //    checkinCheckout.Checkin = DateTimeOffset.Now;
            //    db.CheckinCheckouts.Add(checkinCheckout);
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}
            checkinCheckout.ID = Guid.NewGuid();
            checkinCheckout.PhoneNumberID = db.People.Find(id).PhoneNumberID;
            checkinCheckout.Checkin = DateTimeOffset.Now;
            db.CheckinCheckouts.Add(checkinCheckout);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

            //return View(checkinCheckout);
        }
    }
}