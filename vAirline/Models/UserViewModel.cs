using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace vAirline.Models
{
    public class UserViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public UserViewModel()
        {
            RolesChoice = new SelectList(db.Roles, "Name", "Name");
        }

        [Required]
        [StringLength(50)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Role")]
        public string Role { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public List<IdentityRole> Roles { get; set; }

        public SelectList RolesChoice { get; set; }
    }
}