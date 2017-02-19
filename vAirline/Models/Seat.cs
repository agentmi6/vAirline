using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vAirline.Models
{
    public class Seat
    {
        public Seat()
        {
            SeatAvailable = true;
        }

        public int SeatID { get; set; }

        [Required, Display(Name = "Seat Number")]
        public int SeatNumber { get; set; }

        [Required, StringLength(50), Display(Name = "Seat Location")]
        public string SeatLocation { get; set; }

        [Required, Display(Name = "Seat Status")]
        public bool SeatAvailable { get; set; }

        // Model Relationship
        // 1 seat belongs to 1 flight
        // 1 seat belongs to 1 passenger
        public int? FlightID { get; set; }

        public virtual Flight Flight { get; set; }

        public int? PassengerID { get; set; }

        public virtual Passenger Passenger { get; set; }
    }
}