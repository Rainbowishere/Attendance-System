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
using ClosedXML.Excel;

namespace Attendance_System.Controllers
{
    [Authorize]
    public class CheckInCheckOutController : Controller
    {
        private AttendanceDbContext db = new AttendanceDbContext();


        // GET: CheckInCheckOut
        public async Task<ActionResult> Index(string sortOrder)
        {
            //ViewBag.CheckInParm = String.IsNullOrEmpty(sortOrder) ? "checkin_desc" : "";
            //ViewBag.CheckOutSortParm = sortOrder == "CheckOut" ? "checkout_desc" : "CheckOut";
            //ViewBag.NameSortParm = sortOrder == "Name" ? "checkout_desc" : "CheckOut";

            //var checkinCheckouts = from c in db.CheckinCheckouts.Include(c => c.Person).Include(c => c.Department)
            //                       select c;

            var checkinCheckouts = db.CheckinCheckouts.Include(c => c.Person).Include(c => c.Department).OrderByDescending(c => c.Checkin);

            //switch (sortOrder)
            //{
            //    case "checkin_desc":
            //        checkinCheckouts = checkinCheckouts.OrderByDescending(s => s.LastName);
            //        break;
            //    case "CheckOut":
            //        checkinCheckouts = checkinCheckouts.OrderBy(s => s.EnrollmentDate);
            //        break;
            //    case "checkout_desc":
            //        checkinCheckouts = checkinCheckouts.OrderByDescending(s => s.EnrollmentDate);
            //        break;
            //    default:
            //        checkinCheckouts = checkinCheckouts.OrderBy(c => c.Checkin);
            //        break;
            //}

            return View(await checkinCheckouts.ToListAsync());
        }

        // GET: CheckInCheckOut/Details/5
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

        // GET: CheckInCheckOut/Create
        public ActionResult Create()
        {
            ViewBag.PhoneNumberID = new SelectList(db.People, "PhoneNumberID", "FullName");
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Department1");
            return View();
        }

        // POST: CheckInCheckOut/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,PhoneNumberID,Checkin,Checkout,Purpose,Device,Comment,DepartmentID")] CheckinCheckout checkinCheckout)
        {
            if (ModelState.IsValid)
            {
                checkinCheckout.ID = Guid.NewGuid();
                db.CheckinCheckouts.Add(checkinCheckout);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PhoneNumberID = new SelectList(db.People, "PhoneNumberID", "FullName", checkinCheckout.PhoneNumberID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Department1", checkinCheckout.DepartmentID);
            return View(checkinCheckout);
        }

        // GET: CheckInCheckOut/Edit/5
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
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Department1", checkinCheckout.DepartmentID);
            return View(checkinCheckout);
        }

        // POST: CheckInCheckOut/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PhoneNumberID,Checkin,Checkout,Purpose,Device,Comment,DepartmentID")] CheckinCheckout checkinCheckout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkinCheckout).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PhoneNumberID = new SelectList(db.People, "PhoneNumberID", "FullName", checkinCheckout.PhoneNumberID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Department1", checkinCheckout.DepartmentID);
            return View(checkinCheckout);
        }

        // GET: CheckInCheckOut/Delete/5
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

        // POST: CheckInCheckOut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            CheckinCheckout checkinCheckout = await db.CheckinCheckouts.FindAsync(id);
            db.CheckinCheckouts.Remove(checkinCheckout);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: CheckInCheckOut
        public async Task<ActionResult> Reports()
        {
            var checkinCheckouts = db.CheckinCheckouts.Include(c => c.Person).Include(c => c.Department).OrderByDescending(c => c.Checkin).Where(c => c.Person.PhoneNumberID == null);

            ViewBag.SearchStartDate = $"{DateTime.Now.ToShortDateString()} 00:00";
            ViewBag.SearchEndDate = $"{DateTime.Now.ToShortDateString()} 23:59";
            return View(await checkinCheckouts.ToListAsync());
        }

        //// POST: CheckInCheckOut/Delete/5
        //[HttpPost, ActionName("Reports")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Reports(Guid id)
        //{
        //    CheckinCheckout checkinCheckout = await db.CheckinCheckouts.FindAsync(id);
        //    db.CheckinCheckouts.Remove(checkinCheckout);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        // POST: CheckInCheckOut/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reports(DateTime startdate, DateTime enddate, bool? isNotSignOut, string people, string search)
        {

            ViewBag.SearchStartDate = string.Format("{0:dd/MM/yyy HH:mm}", startdate);
            ViewBag.SearchEndDate = string.Format("{0:dd/MM/yyy HH:mm}", enddate);
            ViewBag.SearchString = search;

           
            if (isNotSignOut != true)
            {
                if (people == "employee")
                {
                    var checkinCheckout = db.CheckinCheckouts.Include(c => c.Person).Include(c => c.Department).Where(c => c.Checkin >= startdate && c.Checkin <= enddate && (c.Person.FullName.Contains(search) || c.Person.PhoneNumberID.Contains(search) || c.Purpose.Contains(search) || c.Person.EmployeeID.Contains(search) || c.Device.Contains(search)  || c.Comment.Contains(search)) && c.Person.EmployeeID != null);
                    return View(checkinCheckout.ToList());
                }
                else if (people == "other")
                {
                    var checkinCheckout = db.CheckinCheckouts.Include(c => c.Person).Include(c => c.Department).Where(c => c.Checkin >= startdate && c.Checkin <= enddate && (c.Person.FullName.Contains(search) || c.Person.PhoneNumberID.Contains(search) || c.Purpose.Contains(search) || c.Person.EmployeeID.Contains(search) || c.Device.Contains(search)  || c.Comment.Contains(search)) && c.Person.EmployeeID == null);
                    return View(checkinCheckout.ToList());
                }
                else
                {
                    var checkinCheckout = db.CheckinCheckouts.Include(c => c.Person).Include(c => c.Department).Where(c => c.Checkin >= startdate && c.Checkin <= enddate && (c.Person.FullName.Contains(search) || c.Person.PhoneNumberID.Contains(search) || c.Purpose.Contains(search) || c.Person.EmployeeID.Contains(search) || c.Device.Contains(search)  || c.Comment.Contains(search)));
                    return View(checkinCheckout.ToList());
                }
            }
            else if (isNotSignOut == true)
            {
                if (people == "employee")
                {
                    var checkinCheckout = db.CheckinCheckouts.Include(c => c.Person).Include(c => c.Department).Where(c => c.Checkin >= startdate && c.Checkin <= enddate && (c.Person.FullName.Contains(search) || c.Person.PhoneNumberID.Contains(search) || c.Purpose.Contains(search) || c.Person.EmployeeID.Contains(search) || c.Device.Contains(search)  || c.Comment.Contains(search)) && c.Checkout == null && c.Person.EmployeeID != null);
                    return View(checkinCheckout.ToList());
                }
                else if (people == "other")
                {
                    var checkinCheckout = db.CheckinCheckouts.Include(c => c.Person).Include(c => c.Department).Where(c => c.Checkin >= startdate && c.Checkin <= enddate && (c.Person.FullName.Contains(search) || c.Person.PhoneNumberID.Contains(search) || c.Purpose.Contains(search) || c.Person.EmployeeID.Contains(search) || c.Device.Contains(search)  || c.Comment.Contains(search)) && c.Checkout == null && c.Person.EmployeeID == null);
                    return View(checkinCheckout.ToList());
                }
                else
                {
                    var checkinCheckout = db.CheckinCheckouts.Include(c => c.Person).Include(c => c.Department).Where(c => c.Checkin >= startdate && c.Checkin <= enddate && (c.Person.FullName.Contains(search) || c.Person.PhoneNumberID.Contains(search) || c.Purpose.Contains(search) || c.Person.EmployeeID.Contains(search) || c.Device.Contains(search)  || c.Comment.Contains(search)) && c.Checkout == null);
                    return View(checkinCheckout.ToList());
                }
                
            }
            else
            {
                var checkinCheckout = db.CheckinCheckouts.Include(c => c.Person).Include(c => c.Department).Where(c => c.Checkin >= startdate && c.Checkin <= enddate && (c.Person.FullName.Contains(search) || c.Person.PhoneNumberID.Contains(search) || c.Purpose.Contains(search) || c.Person.EmployeeID.Contains(search) || c.Device.Contains(search)  || c.Comment.Contains(search)));
                return View(checkinCheckout.ToList());
            }
            
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
