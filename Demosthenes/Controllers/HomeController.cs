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
            var id = User.Identity.GetUserId();

            ViewBag.CurrentUser       = UserManager.FindById(id);
            ViewBag.EnrollableClasses = db.Classes.Where(m => m.Enrollable).Count();
            ViewBag.posts             = db.Posts
                .Where(p => p.Visible || p.AuthorId == id)
                .OrderByDescending(p => p.DateCreated)
                .Take(10).Include(p => p.Author);

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