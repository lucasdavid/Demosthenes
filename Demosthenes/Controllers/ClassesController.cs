using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Demosthenes.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Demosthenes.Controllers
{
    public class ClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected UserManager<ApplicationUser> UserManager { get; set; }

        ClassesController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Classes
        public async Task<ActionResult> Index()
        {
            var classes = db.Classes.Include(c => c.Course).Include(c => c.Professor);
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
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title");
            ViewBag.ProfessorId = new SelectList(db.Professors, "Id", "Name");
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CourseId,ProfessorId,Size,Year,Term,Enrollable")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(@class);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", @class.CourseId);
            ViewBag.ProfessorId = new SelectList(db.Professors, "Id", "Name", @class.ProfessorId);
            return View(@class);
        }

        // GET: Classes/Edit/5
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

        // GET: Classes/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Class @class = await db.Classes.FindAsync(id);
            db.Classes.Remove(@class);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Enroll()
        {
            var classes = db.Classes.Include(c => c.Course).Include(c => c.Professor);
            return View(await classes.ToListAsync());
        }

        // POST: Classes/Enroll/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Enroll([Bind(Include = "Id")] Class @class)
        {
            @class = await db.Classes.FindAsync(@class.Id);

            if (@class.Enroll(await db.Students.FindAsync(User.Identity.GetUserId())))
            {
                db.Entry(@class).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Enroll");
        }

        // POST: Classes/Unenroll/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Unenroll([Bind(Include = "Id")] Class @class)
        {
            @class = await db.Classes.FindAsync(@class.Id);

            @class.Students.Remove(await db.Students.FindAsync(User.Identity.GetUserId()));
            db.Entry(@class).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToAction("Enroll");
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
