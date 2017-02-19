using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vAirline.Models
{
    public class Plane
    {
        [ScaffoldColumn(false)]
        public int PlaneID { get; set; }

        [Required, StringLength(50), Display(Name = "Plane Model")]
        public string PlaneModel { get; set; }

        // First Class Seat
        [Required, Display(Name = "Numbers of Left Seat")]
        public int LeftSeat { get; set; }

        // Business Class Seat 
        [Required, Display(Name = "Numbers of Middle Seat")]
        public int MiddleSeat { get; set; }

        // Economic Class Seat 
        [Required, Display(Name = "Numbers of Right Seat")]
        public int RightSeat { get; set; }

        // Model Relationship
        // 1 plane carries many flights
        public virtual ICollection<Flight> Flights { get; set; }
    }
}