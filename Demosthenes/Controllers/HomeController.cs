using Demosthenes.Models;
using System.Web.Mvc;

namespace Demosthenes.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.OpenedClasses = 10;
            ViewBag.posts = db.Posts.Include("Author");
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