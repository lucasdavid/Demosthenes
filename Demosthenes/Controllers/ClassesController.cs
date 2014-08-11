using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Demosthenes.Core.Models;
using Demosthenes.Core.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Demosthenes.Controllers
{
    [Authorize]
    public class ClassesController : Controller
    {
        private ApplicationDbContext db { get; set; }
        private UserManager<Student> UserManager { get; set; }

        public ClassesController()
        {
            db = new ApplicationDbContext();
            UserManager = new UserManager<Student>(new UserStore<Student>(db));
        }

        public ClassesController(ApplicationDbContext _db, UserManager<Student> _userManager)
        {
            db = _db;
            UserManager = _userManager;
        }

        // GET: Classes
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Index()
        {
            var classes = db.Classes
                .OrderBy(c => c.CourseId)
                .Include(c => c.Course)
                .Include(c => c.Professor);

            return View(await classes.ToListAsync());
        }

        // GET: Classes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.FindAsync(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: Classes/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.CourseId    = new SelectList(db.Courses, "Id", "Title");
            ViewBag.ProfessorId = new SelectList(db.Professors, "Id", "Name");

            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([Bind(Include = "Id,CourseId,ProfessorId,Size,Year,Term,Enrollable")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(@class);
                await db.SaveChangesAsync();
                // TODO: test this
                return RedirectToAction("Schedule", new { id = @class.Id });
            }

            ViewBag.CourseId    = new SelectList(db.Courses, "Id", "Title", @class.CourseId);
            ViewBag.ProfessorId = new SelectList(db.Professors, "Id", "Name", @class.ProfessorId);
            return View(@class);
        }

        // GET: Classes/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.FindAsync(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", @class.CourseId);
            ViewBag.ProfessorId = new SelectList(db.Professors, "Id", "Name", @class.ProfessorId);
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CourseId,ProfessorId,Size,Year,Term,Enrollable")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", @class.CourseId);
            ViewBag.ProfessorId = new SelectList(db.Professors, "Id", "Name", @class.ProfessorId);
            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Class @class = await db.Classes.FindAsync(id);
            db.Classes.Remove(@class);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "student")]
        public async Task<ActionResult> Enroll()
        {
            ViewBag.CurrentStudent = await db.Students.FindAsync(User.Identity.GetUserId());

            var classes = db.Classes
                .Where(c => c.Enrollable == true)
                .OrderBy(c => c.CourseId)
                .Include(c => c.Course)
                .Include(c => c.Professor)
                .Include(c => c.Students);

            return View(await classes.ToListAsync());
        }

        // POST: Classes/Enroll/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "student")]
        public async Task<ActionResult> Enroll([Bind(Include = "Id")] Class @class)
        {
            var student = await db.Students.FindAsync(User.Identity.GetUserId());
            @class      = await db.Classes.FindAsync(@class.Id);
            
            if (@class.Enrollable && @class.Students.Count < @class.Size)
            {
                @class.Students.Add(student);
                db.Entry(@class).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Enroll");
        }

        // POST: Classes/Unenroll/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "student")]
        public async Task<ActionResult> Unenroll([Bind(Include = "Id")] Class @class)
        {
            @class = await db.Classes.FindAsync(@class.Id);

            @class.Students.Remove(await db.Students.FindAsync(User.Identity.GetUserId()));
            db.Entry(@class).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToAction("Enroll");
        }

        // GET: Classes/Calendar
        [Authorize(Roles = "student")]
        public async Task<ActionResult> Calendar(int? year, Term? term)
        {
            ViewBag.year = year = year ?? DateTime.Now.Year;
            ViewBag.term = term = term ?? (Term)(DateTime.Now.Month / 4);

            var id = User.Identity.GetUserId();
            var classes = await db.Classes
                          .Where(c => c.Students.Any(s => s.Id == id) && c.Year == year && c.Term == term)
                          .Include(c => c.Course).Include(c => c.Professor).Include(c => c.Schedules)
                          .ToListAsync();

            return View(new CalendarViewModel(classes));
        }

        // GET: Classes/Schedule/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Schedule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.FindAsync(id);
            if (@class == null)
            {
                return HttpNotFound();
            }

            ViewBag.schedules = await db.Schedules
                .OrderBy(s => s.Day)
                .ToListAsync();

            return View(new ClassSchedulesViewModel(@class));
        }

        // POST: Classes/Schedule/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Schedule([Bind(Include = "Id,Schedules")] ClassSchedulesViewModel model)
        {
            if (ModelState.IsValid)
            {
                Class @class = await db.Classes.FindAsync(model.Id);
                
                @class.Schedules.Clear();

                foreach (int scheduleId in model.Schedules)
                {
                    var schedule = new Schedule { Id = scheduleId };
                    
                    db.Schedules.Attach(schedule);
                    @class.Schedules.Add(schedule);
                }
                
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
