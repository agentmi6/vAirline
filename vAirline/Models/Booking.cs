using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vAirline.Models
{
    public class Booking
    {
        [ScaffoldColumn(false)]
        public int BookingID { get; set; }

        [Required, Display(Name = "Booking Date & Time")]
        public DateTime BookingDateTime { get; set; }

        [Required, Display(Name = "Price")]
        public double BookingPrice { get; set; }

        // Model Relationship
        // 1 booking belongs to 1 passenger
        // 1 booking belongs to 1 flight
        // 1 booking has 1 seat
        public int? PassengerID { get; set; }

        public virtual Passenger Passenger { get; set; }

        public int? FlightID { get; set; }

        public virtual Flight Flight { get; set; }

        public int? SeatID { get; set; }

        public virtual Seat Seat { get; set; }
    }
}