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
<<<<<<< HEAD
            var isCheckedIn = db.CheckinCheckouts.Where(x => x.PhoneNumberID == id && x.Checkin != null && x.Checkout == null).FirstOrDefault();
=======
>>>>>>> 997cf7e6fbbbab7083ed2969e085157d099fd1e0

            if (existingUser == null)
            {
                return PartialView("Register");
            }
            else
            {
<<<<<<< HEAD
                if (isCheckedIn == null)
                {
                    return PartialView(existingUser);
                }
                else
                {
                    return PartialView("Checkout", isCheckedIn);
                }

=======
                return PartialView(existingUser);
>>>>>>> 997cf7e6fbbbab7083ed2969e085157d099fd1e0
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