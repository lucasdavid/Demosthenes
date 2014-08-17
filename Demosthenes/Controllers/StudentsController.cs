using Demosthenes.Core.Models;
using Demosthenes.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Demosthenes.Controllers
{
    [Authorize(Roles = "admin, professor")]
    public class StudentsController : Controller
    {
        private ApplicationDbContext db { get; set; }
        private UserManager<Student> UserManager { get; set; }

        public StudentsController()
        {
            db = new ApplicationDbContext();
            UserManager = new UserManager<Student>(new UserStore<Student>(db));
        }

        public StudentsController(ApplicationDbContext dbContext, UserManager<Student> userManager)
        {
            db = dbContext;
            UserManager = userManager;
        }

        // GET: Students
        public ActionResult Index(string q = null, int page = 1, int size = 10)
        {
            var students = db.Students
                            .Where(p => q == null || p.Name.Contains(q) || p.Email.Contains(q))
                            .OrderBy(s => s.Name)
                            .ToPagedList(page, size);

            ViewBag.q = q;
            
            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", students);
            }

            return View(students);
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(new StudentViewModel(student));
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var student = new Student(viewModel);
                IdentityResult result = await UserManager.CreateAsync(student, viewModel.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(student.Id, "student");
                    return RedirectToAction("Index");
                }

                AddErrors(result);
            }

            return View(viewModel);
        }

        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(new StudentEditViewModel(student));
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StudentEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var student = new Student(viewModel);
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Student student = await db.Students.FindAsync(id);
            db.Users.Remove(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
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
