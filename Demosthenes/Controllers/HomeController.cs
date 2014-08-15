using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demosthenes.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Demosthenes.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db { get; set; }
        private UserManager<ApplicationUser> UserManager { get; set; }

        public HomeController()
        {
            db = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public HomeController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            db = dbContext;
            UserManager = userManager;
        }

        public ActionResult Index()
        {
            ViewBag.AnyEnrollableClass = db.Classes.Any(m => m.Enrollable);

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}