using Attendance_System.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Attendance_System.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// The dabase model
        /// </summary>
        private AttendanceDbContext db = new AttendanceDbContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Loads user's information from the page to the model
        /// </summary>
        /// <param name="id">The id of the registered users</param>
        /// <returns></returns>
        public ActionResult LoadEmployee(string id)
        {
            var existingUser = db.People.Find(id);
            var isCheckedIn = db.CheckinCheckouts.Where(x => x.PhoneNumberID == id && x.Checkin != null && x.Checkout == null).FirstOrDefault();

            if (existingUser == null)
                return PartialView("Register");
            else
            {
                if (isCheckedIn == null)
                    return PartialView(existingUser);
                
                else
                    return PartialView("Checkout", isCheckedIn);
            }
        }

        /// <summary>
        /// Check in the resgistered users to the system
        /// </summary>
        /// <param name="checkinCheckout">The Check in-out database model</param>
        /// <param name="Purpose2">The other.. purpose that users type in</param>
        /// <param name="Device2">The other.. devices that users type in</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckIn([Bind(Include = "ID,PhoneNumberID,Checkin,Checkout,Purpose,Device,Purpose2")] CheckinCheckout checkinCheckout, string Purpose2,string Device2)
        {
            var otherText = "ອື່ນໆ...";

            if (checkinCheckout.Purpose == otherText)
                checkinCheckout.Purpose = Purpose2;

            if (checkinCheckout.Device == otherText)
                checkinCheckout.Device = Device2;

            if (checkinCheckout.Device == "")
                checkinCheckout.Device = null;

            checkinCheckout.ID = Guid.NewGuid();
            checkinCheckout.Checkin = DateTimeOffset.Now;
            db.CheckinCheckouts.Add(checkinCheckout);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Check-out the users if they already checked-in
        /// </summary>
        /// <param name="checkinCheckout">The Check in-out database model</param>
        /// <returns></returns>
        [HttpPost, ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout([Bind(Include = "ID,PhoneNumberID,Checkin,Checkout,Purpose,Device,Comment")] CheckinCheckout checkinCheckout)
        {
            checkinCheckout.Checkout = DateTimeOffset.Now;
            db.Entry(checkinCheckout).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PhoneNumberID,FullName,Source,EmployeeID,IsActive")] Person person)
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