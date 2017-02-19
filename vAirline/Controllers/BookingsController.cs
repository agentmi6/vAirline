using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using vAirline.Models;

namespace vAirline.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            ViewBag.PassengerUserName = User.Identity.GetUserName();

            var bookings = db.Bookings.Include(b => b.Flight).Include(b => b.Passenger).Include(b => b.Seat);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create(int id)
        {
            BookingViewModel model = new BookingViewModel();
            model.Flight = db.Flights.Find(id);
            TempData["FlightID"] = model.Flight.FlightID;

            return View(model);
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookingViewModel model)
        {
            Flight flight = db.Flights.Find(TempData["FlightID"]);

            try
            {
                // Add passenger
                if (ModelState.IsValid)
                {
                    Passenger passenger;
                    Passenger currPassenger;
                    string username = User.Identity.GetUserName();

                    try
                    {
                        currPassenger = db.Passengers.Where(p => p.PassengerUserName == username).Single();
                        passenger = currPassenger;
                    }
                    catch (System.InvalidOperationException)
                    {
                        passenger = new Passenger
                        {
                            PassengerFullName = model.FullName,
                            PassengerUserName = User.Identity.GetUserName(),
                            PassengerPassportNo = model.PassportNo,
                            PassengerAge = model.Age,
                            PassengerGender = model.Gender
                        };

                        db.Passengers.Add(passenger);
                        db.SaveChanges();
                    }

                    // Add seat
                    int bookingSeat;
                    Seat seatToUpdate;

                    foreach (string seat in model.BookingSeats)
                    {
                        if (!(string.IsNullOrEmpty(seat)))
                        {
                            bookingSeat = int.Parse(seat);

                            seatToUpdate = db.Seats.Where(s => s.SeatNumber == bookingSeat && s.FlightID == flight.FlightID).Single();

                            if (TryUpdateModel(seatToUpdate, "", new string[] { "SeatID", "SeatNumber", "FlightClass", "SeatAvailable", "FlightID", "PassengerID", "BookingPrice" }))
                            {
                                try
                                {
                                    seatToUpdate.SeatAvailable = false;
                                    seatToUpdate.PassengerID = passenger.PassengerID;

                                    if (ModelState.IsValid)
                                    {
                                        db.Entry(seatToUpdate).State = EntityState.Modified;
                                        db.SaveChanges();

                                        // Update seat count
                                        int seatCount;

                                        if (seatToUpdate.SeatLocation.Equals("Left"))
                                        {
                                            seatCount = flight.LeftSeat;
                                            flight.LeftSeat = --seatCount;
                                        }
                                        else if (seatToUpdate.SeatLocation.Equals("Middle"))
                                        {
                                            seatCount = flight.MiddleSeat;
                                            flight.MiddleSeat = --seatCount;
                                        }
                                        else
                                        {
                                            seatCount = flight.RightSeat;
                                            flight.RightSeat = --seatCount;
                                        }

                                        seatCount = flight.TotalSeatsAvailable;
                                        flight.TotalSeatsAvailable = --seatCount;

                                        db.Entry(flight).State = EntityState.Modified;
                                        db.SaveChanges();

                                        // Add booking
                                        Booking booking = new Booking
                                        {
                                            BookingDateTime = DateTime.Now,
                                            BookingPrice = flight.PricePerSeat,
                                            PassengerID = passenger.PassengerID,
                                            FlightID = flight.FlightID,
                                            SeatID = seatToUpdate.SeatID
                                        };

                                        db.Bookings.Add(booking);
                                        db.SaveChanges();
                                    }
                                }
                                catch (DataException)
                                {
                                    ModelState.AddModelError("", "Unable to save changes.");
                                }
                            }
                        }
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                return View(model);
            }

            // Return to previous page if fail
            return View(model);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlightID = new SelectList(db.Flights, "FlightID", "FlightCode", booking.FlightID);
            ViewBag.PassengerID = new SelectList(db.Passengers, "PassengerID", "PassengerFullName", booking.PassengerID);
            ViewBag.SeatID = new SelectList(db.Seats, "SeatID", "SeatLocation", booking.SeatID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,BookingDateTime,PassengerID,FlightID,SeatID,BookingPrice")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlightID = new SelectList(db.Flights, "FlightID", "FlightCode", booking.FlightID);
            ViewBag.PassengerID = new SelectList(db.Passengers, "PassengerID", "PassengerFullName", booking.PassengerID);
            ViewBag.SeatID = new SelectList(db.Seats, "SeatID", "SeatLocation", booking.SeatID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);

            // Change seat status to available
            Seat seat = db.Seats.Find(booking.SeatID);
            seat.SeatAvailable = true;
            seat.PassengerID = null;
            db.Entry(seat).State = EntityState.Modified;
            db.SaveChanges();

            // Remove booking
            db.Bookings.Remove(booking);
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