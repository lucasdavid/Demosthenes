using Demosthenes.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using Demosthenes.Core.ViewModels;
using Demosthenes.Services;
using Demosthenes.Core.Exceptions.Enrollment;

namespace Demosthenes.Controllers
{
    [Authorize]
    public class EnrollmentController : Controller
    {
        private readonly EnrollmentService enrollments;
        private readonly StudentService students;
        private readonly ClassService classes;
        private readonly ScheduleService schedules;
        private readonly UserManager<Student> UserManager;

        public EnrollmentController(EnrollmentService _enrollments, StudentService _students, ClassService _classes, ScheduleService _schedules, UserManager<Student> _userManager)
        {
            enrollments = _enrollments;
            students = _students;
            classes = _classes;
            schedules = _schedules;
            UserManager = _userManager;
        }

        [Authorize(Roles = "student")]
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            ViewBag.CurrentStudent = await students.FindAsync(User.Identity.GetUserId());

            var enrollable = (await classes.EnrollableClasses(q))
                .ToPagedList(page, size);

            ViewBag.q = q;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", enrollable);
            }

            return View(enrollable);
        }

        // POST: Classes/Enroll/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "student")]
        public async Task<ActionResult> Enroll(int classId)
        {
            var student = await students.FindAsync(User.Identity.GetUserId());
            var @class = await classes.FindAsync(classId);
            var enrollment = await enrollments.Enroll(student, @class);

            if (Request.IsAjaxRequest())
            {
                return Json(new { enrollment = "Success" });
            }

            return RedirectToAction("Index");
        }

        // POST: Classes/Unenroll/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "student")]
        public async Task<ActionResult> Unenroll(int enrollmentId)
        {
            var student = await students.FindAsync(User.Identity.GetUserId());
            var enrollment = await enrollments.Unenroll(student, enrollmentId);

            if (Request.IsAjaxRequest())
            {
                return Json(new { unenrollment = "Sucess" });
            }

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

            var student = await students.FindAsync(User.Identity.GetUserId());
            var enrolledClasses = await classes.EnrolledBy(student, year, term);

            // getting possible starting times for schedules
            var times = await schedules.AllStartingTimes();

            return Json(new
            {
                classes = enrolledClasses,
                times = times,
                year = year,
                term = term.ToString()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
