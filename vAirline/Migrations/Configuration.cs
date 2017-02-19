namespace vAirline.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<vAirline.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(vAirline.Models.ApplicationDbContext context)
        {
            AddUserAndRole(context);
            GetPlanes().ForEach(p => context.Planes.Add(p));
            GetAirports().ForEach(a => context.Airports.Add(a));
            GetFlights().ForEach(f => context.Flights.Add(f));
            GetSeats().ForEach(s => context.Seats.Add(s));
        }

        bool AddUserAndRole(ApplicationDbContext context)
        {
            IdentityResult ir;

            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            // Admin
            ir = rm.Create(new IdentityRole("admin"));
            // Member
            ir = rm.Create(new IdentityRole("member"));

            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "admin@vAirline.com",
            };
            ir = um.Create(user, "Password123$");

            if (ir.Succeeded)
            {
                ir = um.AddToRole(user.Id, "admin");
            }

            return ir.Succeeded;
        }

        private static List<Plane> GetPlanes()
        {
            var planes = new List<Plane>
            {
                new Plane
                {
                    PlaneID = 1,
                    PlaneModel = "Airbus A380",
                    LeftSeat = 30,
                    MiddleSeat = 60,
                    RightSeat = 30
                },
                new Plane
                {
                    PlaneID = 2,
                    PlaneModel = "Boeing 747",
                    LeftSeat = 20,
                    MiddleSeat = 40,
                    RightSeat = 20
                }
            };

            return planes;
        }

        private static List<Airport> GetAirports()
        {
            var airports = new List<Airport>
            {
                new Airport
                {
                    AirportID = 1,
                    AirportName = "Barcelona–El Prat Airport",
                    AirportCode = "BCN",
                    AirportCountry = "Spain"
                },
                new Airport
                {
                    AirportID = 2,
                    AirportName = "Malmö Airport",
                    AirportCode = "MMX",
                    AirportCountry = "Sweden"
                }
            };

            return airports;
        }
        private static List<Flight> GetFlights()
        {
            var flights = new List<Flight>
            {
                new Flight
                {
                    FlightID = 1,
                    FlightCode = "MS1",
                    DepartureAirportCode = "BCN",
                    DepartureDateTime = DateTime.Now,
                    ArrivalAirportCode= "MMX",
                    ArrivalDateTime = DateTime.Now,
                    PricePerSeat = 60,
                    LeftSeat = 30,
                    MiddleSeat = 60,
                    RightSeat = 30,
                    TotalSeatsAvailable = 120,
                    PlaneID = 1
                }
            };
            return flights;
        }
        private static List<Seat> GetSeats()
        {
            Seat seat;
            String seatLocation = "Left";
            var seats = new List<Seat>();

            // TotalSeat
            for (int i = 0; i < 120; i++)
            {
                if (i > 29 && i < 90)
                {
                    seatLocation = "Middle";
                }
                else if (i > 89)
                {
                    seatLocation = "Right";
                }
                seat = new Seat
                {
                    SeatID = i + 1,
                    SeatNumber = i + 1,
                    SeatLocation = seatLocation,
                    FlightID = 1
                };

                seats.Add(seat);
            }
            return seats;
        }
    }
}

