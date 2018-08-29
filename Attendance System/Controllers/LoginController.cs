using Attendance_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            var isCheckedIn = db.CheckinCheckouts.Where(x => x.PhoneNumberID == id && x.Checkin != null && x.Checkout == null).FirstOrDefault();

            if (existingUser == null)
            {
                return PartialView("Register");
            }
            else
            {
                if (isCheckedIn == null)
                {
                    return PartialView(existingUser);
                }
                else
                {
                    return PartialView("Checkout", isCheckedIn);
                }

            }

            //var person = db.People.Find(id);
            
        }

        // POST: CheckinCheckouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckIn([Bind(Include = "ID,PhoneNumberID,Checkin,Checkout,Purpose,Device,Purpose2")] CheckinCheckout checkinCheckout, string Purpose2,string Device2)
        {
            if (checkinCheckout.Purpose == "ອື່ນໆ...")
            {
                checkinCheckout.Purpose = Purpose2;
            }


            if (checkinCheckout.Device == "ອື່ນໆ...")
            {
                checkinCheckout.Device = Device2;
            }

            if (checkinCheckout.Device == "")
            {
                checkinCheckout.Device = null;
            }
            checkinCheckout.ID = Guid.NewGuid();
            //checkinCheckout.PhoneNumberID = db.People.Find(id).PhoneNumberID;
            checkinCheckout.Checkin = DateTimeOffset.Now;
            db.CheckinCheckouts.Add(checkinCheckout);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

            //return View(checkinCheckout);
        }



        // POST: CheckinCheckouts1/Delete/5
        [HttpPost, ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout([Bind(Include = "ID,PhoneNumberID,Checkin,Checkout,Purpose,Device,Comment")] CheckinCheckout checkinCheckout)
        {
            checkinCheckout.Checkout = DateTimeOffset.Now;
            db.Entry(checkinCheckout).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}