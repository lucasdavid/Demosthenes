using Demosthenes.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;

namespace Demosthenes.Controllers
{
    [Authorize]
    public class EnrollmentController : Controller
    {
        private ApplicationDbContext db { get; set; }
        private UserManager<Student> UserManager { get; set; }

        public EnrollmentController()
        {
            db = new ApplicationDbContext();
            UserManager = new UserManager<Student>(new UserStore<Student>(db));
        }

        public EnrollmentController(ApplicationDbContext _db, UserManager<Student> _userManager)
        {
            db = _db;
            UserManager = _userManager;
        }

        [Authorize(Roles = "student")]
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            ViewBag.CurrentStudent = await db.Students.FindAsync(User.Identity.GetUserId());

            var classes = db.Classes
                .Where(c => c.Enrollable == true && (q == null || c.Course.Title.Contains(q) || c.Professor.Name.Contains(q)))
                .OrderBy(c => c.CourseId)
                .ThenByDescending(c => c.Id)
                .Include(c => c.Course)
                .Include(c => c.Professor)
                .Include(c => c.Enrollment)
                .Include(c => c.Schedules)
                .ToPagedList(page, size);

            ViewBag.q = q;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", classes);
            }

            return View(classes);
        }

        // POST: Classes/Enroll/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "student")]
        public async Task<ActionResult> Index([Bind(Include = "Id")] Class @class)
        {
            var student = await db.Students.FindAsync(User.Identity.GetUserId());
            @class = await db.Classes.FindAsync(@class.Id);

            var enrollment = new Enrollment(student, @class);
            db.Enrollments.Add(enrollment);
            await db.SaveChangesAsync();

            var classes = db.Classes
                .Where(c => c.Enrollable == true)
                .OrderBy(c => c.CourseId)
                .ThenBy(c => c.Course.Title)
                .Include(c => c.Course)
                .Include(c => c.Professor)
                .Include(c => c.Enrollment)
                .Include(c => c.Schedules);

            return View(await classes.ToListAsync());
        }

        // POST: Classes/Unenroll/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "student")]
        public async Task<ActionResult> Unenroll([Bind(Include = "Id")] Class @class)
        {
            var id = User.Identity.GetUserId();

            var enrollment = await db.Enrollments
                .Where(e => e.Class.Enrollable && e.StudentId == id && e.ClassId == @class.Id)
                .FirstAsync();

            db.Enrollments.Remove(enrollment);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Classes/Calendar
        [Authorize(Roles = "student")]
        public async Task<ActionResult> Calendar(int? year, Term? term)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index");
            }

            // if year not informed, get current year
            year = year ?? DateTime.Now.Year;
            // if term not informed, get current term
            term = term ?? (Term)(DateTime.Now.Month / 4);

            // get classes from @year and @term that were signed by the student @id
            var id = User.Identity.GetUserId();
            var classes = await db.Classes
                .Where(c => c.Year == year && c.Term == term && c.Enrollment.Any(e => e.Student.Id == id))
                .Include(c => c.Course)
                .Include(c => c.Schedules)
                          .Select(c => new
                          {
                              Id = c.Id,
                              CourseTitle = c.Course.Title,
                              Schedules = c.Schedules.Select(s => new
                              {
                                  Id = s.Id,
                                  Day = s.Day,
                                  Starting = s.Starting,
                                  Ending = s.Ending
                              })
                          })
                          .ToListAsync();

            // getting possible starting times for schedules
            var times = await db.Schedules
                .GroupBy(s => s.Starting)
                .Select(g => g.Key)
                .ToListAsync();

            return Json(new
            {
                classes = classes,
                times = times,
                year = year,
                term = term.ToString()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
