using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Attendance_System.Models;

namespace Attendance_System.Controllers
{
    public class CheckinCheckouts1Controller : Controller
    {
        private AttendanceDbContext db = new AttendanceDbContext();

        // GET: CheckinCheckouts1
        public async Task<ActionResult> Index()
        {
            var checkinCheckouts = db.CheckinCheckouts.Include(c => c.Person);
            return View(await checkinCheckouts.ToListAsync());
        }

        // GET: CheckinCheckouts1/Details/5
        public async Task<ActionResult> Details(Guid? id)
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

        // GET: CheckinCheckouts1/Create
        public ActionResult Create()
        {
            ViewBag.PhoneNumberID = new SelectList(db.People, "PhoneNumberID", "FullName");
            return View();
        }

        // POST: CheckinCheckouts1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,PhoneNumberID,Checkin,Checkout,Purpose,Device")] CheckinCheckout checkinCheckout)
        {
            if (ModelState.IsValid)
            {
                checkinCheckout.ID = Guid.NewGuid();
                db.CheckinCheckouts.Add(checkinCheckout);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PhoneNumberID = new SelectList(db.People, "PhoneNumberID", "FullName", checkinCheckout.PhoneNumberID);
            return View(checkinCheckout);
        }

        // GET: CheckinCheckouts1/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
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
            ViewBag.PhoneNumberID = new SelectList(db.People, "PhoneNumberID", "FullName", checkinCheckout.PhoneNumberID);
            return View(checkinCheckout);
        }

        // POST: CheckinCheckouts1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PhoneNumberID,Checkin,Checkout,Purpose,Device")] CheckinCheckout checkinCheckout)
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

        // GET: CheckinCheckouts1/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
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

        // POST: CheckinCheckouts1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            CheckinCheckout checkinCheckout = await db.CheckinCheckouts.FindAsync(id);
            db.CheckinCheckouts.Remove(checkinCheckout);
            await db.SaveChangesAsync();
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
