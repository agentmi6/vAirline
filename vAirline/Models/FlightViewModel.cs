using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace vAirline.Models
{
    public class FlightViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public FlightViewModel()
        {
            DepartureDateTime = DateTime.Now;
            ArrivalDateTime = DateTime.Now;
            Airports = new SelectList(db.Airports, "AirportID", "AirportName");
            Planes = new SelectList(db.Planes, "PlaneID", "PlaneModel");
        }

        [Required]
        [StringLength(10)]
        [Display(Name = "Departure (From)")]
        public string DepartureAirportID { get; set; }

        [Required]
        [Display(Name = "Departure Time")]
        public DateTime DepartureDateTime { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Arrival (To)")]
        public string ArrivalAirportID { get; set; }

        [Required]
        [Display(Name = "Arrival Time")]
        public DateTime ArrivalDateTime { get; set; }

        [Required]
        [Display(Name = "Price / Seat")]
        public double PricePerSeat { get; set; }

        [Required]
        [Display(Name = "Seats Available (Left)")]
        public int LeftSeat { get; set; }

        [Required]
        [Display(Name = "Seats Available (Middle)")]
        public int MidSeat { get; set; }

        [Required]
        [Display(Name = "Seats Available (Right)")]
        public int RightSeat { get; set; }

        [Required]
        [Display(Name = "Total Seats Available")]
        public int TotalSeatsAvailable { get; set; }

        [Required]
        [Display(Name = "Plane Model")]
        public int PlaneID { get; set; }

        public SelectList Airports { get; set; }

        public SelectList Planes { get; set; }
    }
}