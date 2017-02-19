using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vAirline.Models
{
    public class Flight
    {
        [ScaffoldColumn(false)]
        public int FlightID { get; set; }

        [ScaffoldColumn(false), StringLength(50), Display(Name = "Flight Code")]
        public string FlightCode { get; set; }

        [Required, StringLength(50), Display(Name = "Departure (From)")]
        public string DepartureAirportCode { get; set; }

        [Required, Display(Name = "Departure Date & Time")]
        public DateTime DepartureDateTime { get; set; }

        [Required, StringLength(50), Display(Name = "Arrival (To)")]
        public string ArrivalAirportCode { get; set; }

        [Required, Display(Name = "Arrival Date & Time")]
        public DateTime ArrivalDateTime { get; set; }

        [Required, Display(Name = "Price / Seat")]
        public double PricePerSeat { get; set; }

        [Required, Display(Name = "Seats Available (Left)")]
        public int LeftSeat { get; set; }

        [Required, Display(Name = "Seats Available (Middle)")]
        public int MiddleSeat { get; set; }

        [Required, Display(Name = "Seats Available (Right)")]
        public int RightSeat { get; set; }

        // Total seats available
        [Display(Name = "Total Seats Available")]
        public int TotalSeatsAvailable { get; set; }

        // Model Relationship
        // 1 flight is carried by 1 plane
        // 1 flight contains many bookings
        // 1 flight contains many seats
        public int PlaneID { get; set; }

        public virtual Plane Plane { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}