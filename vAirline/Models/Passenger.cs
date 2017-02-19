using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vAirline.Models
{
    public class Passenger
    {
        [ScaffoldColumn(false)]
        public int PassengerID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Username")]
        public string PassengerUserName { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Full Name (according to Passport)")]
        public string PassengerFullName { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Passport Number")]
        public string PassengerPassportNo { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int PassengerAge { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Gender")]
        public string PassengerGender { get; set; }

        // Model Relationship
        // 1 Passenger can have many booking
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}