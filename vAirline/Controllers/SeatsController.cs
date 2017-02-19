using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using vAirline.Models;

namespace vAirline.Controllers
{

    [Authorize(Roles = "admin")]
    public class SeatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Seats
        public ActionResult Index()
        {
            var seats = db.Seats.Include(s => s.Flight).Include(s => s.Passenger);
            return View(seats.ToList());
        }

        // GET: Seats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seat seat = db.Seats.Find(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            return View(seat);
        }

        // GET: Seats/Create
        public ActionResult Create()
        {
            ViewBag.FlightID = new SelectList(db.Flights, "FlightID", "FlightCode");
            ViewBag.PassengerID = new SelectList(db.Passengers, "PassengerID", "PassengerFullName");
            return View();
        }

        // POST: Seats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SeatID,SeatNumber,SeatLocation,SeatAvailable,FlightID,PassengerID")] Seat seat)
        {
            if (ModelState.IsValid)
            {
                db.Seats.Add(seat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FlightID = new SelectList(db.Flights, "FlightID", "FlightCode", seat.FlightID);
            ViewBag.PassengerID = new SelectList(db.Passengers, "PassengerID", "PassengerFullName", seat.PassengerID);
            return View(seat);
        }

        // GET: Seats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seat seat = db.Seats.Find(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlightID = new SelectList(db.Flights, "FlightID", "FlightCode", seat.FlightID);
            ViewBag.PassengerID = new SelectList(db.Passengers, "PassengerID", "PassengerFullName", seat.PassengerID);
            return View(seat);
        }

        // POST: Seats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeatID,SeatNumber,SeatLocation,SeatAvailable,FlightID,PassengerID")] Seat seat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlightID = new SelectList(db.Flights, "FlightID", "FlightCode", seat.FlightID);
            ViewBag.PassengerID = new SelectList(db.Passengers, "PassengerID", "PassengerFullName", seat.PassengerID);
            return View(seat);
        }

        // GET: Seats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seat seat = db.Seats.Find(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            return View(seat);
        }

        // POST: Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seat seat = db.Seats.Find(id);
            db.Seats.Remove(seat);
            db.SaveChanges();
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