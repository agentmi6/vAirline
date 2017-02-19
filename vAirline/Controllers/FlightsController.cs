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
    public class FlightsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Flights
        public ActionResult Index()
        {

            var flights = db.Flights.Include(f => f.Plane);
            return View(flights.ToList());
        }

        // GET: Flights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET: Flights/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            FlightViewModel model = new FlightViewModel();
            ViewBag.Planes = db.Planes.ToArray();

            return View(model);
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlightViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find chosen departure and arrival airports
                Airport departureAirport = db.Airports.Find(int.Parse(model.DepartureAirportID));
                Airport arrivalAirport = db.Airports.Find(int.Parse(model.ArrivalAirportID));

                // Add new flight to database
                Flight flight = new Flight
                {

                    DepartureAirportCode = departureAirport.AirportCode,
                    DepartureDateTime = model.DepartureDateTime,
                    ArrivalAirportCode = arrivalAirport.AirportCode,
                    ArrivalDateTime = model.ArrivalDateTime,
                    PricePerSeat = model.PricePerSeat,
                    LeftSeat = model.LeftSeat,
                    MiddleSeat = model.MidSeat,
                    RightSeat = model.RightSeat,
                    TotalSeatsAvailable = model.TotalSeatsAvailable,
                    PlaneID = model.PlaneID
                };

                db.Flights.Add(flight);
                db.SaveChanges();

                // Generate flight code based on countries
                string flightID = flight.FlightID.ToString();
                string departureCountry = departureAirport.AirportCountry[0].ToString();
                string arrivalCountry = arrivalAirport.AirportCountry[0].ToString();

                flight.FlightCode = departureCountry + arrivalCountry + flightID;

                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();

                // Generate seats for new flight
                int leftSeatCount = flight.LeftSeat;
                int leftMidSeatCount = leftSeatCount + flight.MiddleSeat;
                int totalSeatCount = flight.TotalSeatsAvailable;

                Seat seat;

                for (int i = 1; i <= totalSeatCount; i++)
                {
                    seat = new Seat
                    {
                        SeatNumber = i,
                        SeatLocation = "Left",
                        FlightID = flight.FlightID
                    };

                    if (i > leftSeatCount && i <= leftMidSeatCount)
                    {
                        seat.SeatLocation = "Middle";
                    }
                    else if (i > leftMidSeatCount)
                    {
                        seat.SeatLocation = "Right";
                    }

                    db.Seats.Add(seat);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.Planes = db.Planes.ToArray();
            return View(model);
        }

        [Authorize(Roles = "admin")]
        // GET: Flights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaneID = new SelectList(db.Planes, "PlaneID", "PlaneModel", flight.PlaneID);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlightID,FlightCode,DepartureAirportCode,DepartureDateTime,ArrivalAirportCode,ArrivalDateTime,PricePerSeat,LeftSeat,MiddleSeat,RightSeat,TotalSeatsAvailable,PlaneID")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaneID = new SelectList(db.Planes, "PlaneID", "PlaneModel", flight.PlaneID);
            return View(flight);
        }

        // GET: Flights/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
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