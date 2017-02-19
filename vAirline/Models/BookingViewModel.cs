using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace vAirline.Models
{
    public class BookingViewModel
    {
        public BookingViewModel()
        {
            // Initialize booking seats array list
            int count = 3;

            BookingSeats = new List<string>();

            for (int i = 0; i < count; i++)
            {
                BookingSeats.Add("");
            }

            // Initialize gender select list for drop down box
            Genders = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Male",
                    Value = "Male",
                    Selected = true
                },
                new SelectListItem()
                {
                    Text  = "Female",
                    Value = "Female"
                }
            };
        }

        public Flight Flight { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Full Name (according to passport)")]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Passport No.")]
        public string PassportNo { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Amount Payable (RM)")]
        public double AmountPayable { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public List<string> BookingSeats { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public List<SelectListItem> Genders { get; set; }
    }
}