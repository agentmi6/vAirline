using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vAirline.Models
{
    public class Airport
    {
        [ScaffoldColumn(false)]
        public int AirportID { get; set; }

        [Required, StringLength(250), Display(Name = "Airport Name")]
        public string AirportName { get; set; }

        [Required, StringLength(50), Display(Name = "Airport Code")]
        public string AirportCode { get; set; }

        [Required, StringLength(250), Display(Name = "Airport Country")]
        public string AirportCountry { get; set; }
    }
}