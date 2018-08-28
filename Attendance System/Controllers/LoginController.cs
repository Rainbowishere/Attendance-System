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
        public async Task<ActionResult> CheckIn([Bind(Include = "ID,PhoneNumberID,Checkin,Checkout,Purpose,Device,Purpose2")] CheckinCheckout checkinCheckout, string Purpose2)
        {
            //if (ModelState.IsValid)
            //{
            //    checkinCheckout.ID = Guid.NewGuid();
            //    checkinCheckout.Checkin = DateTimeOffset.Now;
            //    db.CheckinCheckouts.Add(checkinCheckout);
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}

            if (checkinCheckout.Purpose == "ອື່ນໆ...")
            {
                checkinCheckout.Purpose = Purpose2;
            }

            checkinCheckout.ID = Guid.NewGuid();
            //checkinCheckout.PhoneNumberID = db.People.Find(id).PhoneNumberID;
            checkinCheckout.Checkin = DateTimeOffset.Now;
            db.CheckinCheckouts.Add(checkinCheckout);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

            //return View(checkinCheckout);
        }

        // GET: CheckinCheckouts1/Details/5
        public async Task<ActionResult> Checkout(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckinCheckout checkinCheckout = await db.CheckinCheckouts.FindAsync(id);
            if (checkinCheckout == null)
            {
                return HttpNotFound();
            }
            return View(checkinCheckout);
        }

        // POST: CheckinCheckouts1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout([Bind(Include = "ID,PhoneNumberID,Checkin,Checkout,Purpose,Device")] CheckinCheckout checkinCheckout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkinCheckout).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PhoneNumberID = new SelectList(db.People, "PhoneNumberID", "FullName", checkinCheckout.PhoneNumberID);
            return View(checkinCheckout);
        }

    }
}