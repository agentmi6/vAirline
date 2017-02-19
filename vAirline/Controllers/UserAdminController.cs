using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vAirline.Models;

namespace vAirline.Controllers
{
    public class UserAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserAdmin
        public ActionResult Index()
        {
            UserViewModel model = new UserViewModel();
            model.Users = db.Users.ToList();
            model.Roles = db.Roles.ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            UserViewModel model = new UserViewModel();

            return View(model);
        }
    }
}