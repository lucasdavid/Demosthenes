using Demosthenes.Core.Models;
using Demosthenes.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;

namespace Demosthenes.Controllers
{
    [Authorize]
    public class ProfessorsController : Controller
    {
        private ApplicationDbContext db { get; set; }
        private UserManager<Professor> UserManager { get; set; }

        public ProfessorsController()
        {
            db = new ApplicationDbContext();
            UserManager = new UserManager<Professor>(new UserStore<Professor>(db));
        }

        public ProfessorsController(ApplicationDbContext dbContext, UserManager<Professor> userManager)
        {
            db = dbContext;
            UserManager = userManager;
        }

        // GET: Professors
        public ActionResult Index(string q = null, int page = 1, int size = 10)
        {
            var professors = db.Professors
                            .Where(p => q == null || p.Name.Contains(q) || p.Email.Contains(q))
                            .OrderBy(p => p.DepartmentId)
                            .ThenBy(p => p.Name)
                            .Include(p => p.Department)
                            .ToPagedList(page, size);

            ViewBag.q = q;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", professors);
            }

            return View(professors);
        }

        // GET: Professors/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = await db.Professors.FindAsync(id);

            if (professor == null)
            {
                return HttpNotFound();
            }
            // TODO: Department entity should eagerly load
            return View(new ProfessorViewModel(professor));
        }

        // GET: Professors/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            return View();
        }

        // POST: Professors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(ProfessorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var professor = new Professor(viewModel);
                IdentityResult result = await UserManager.CreateAsync(professor, viewModel.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(professor.Id, "professor");
                    return RedirectToAction("Index");
                }

                AddErrors(result);
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", viewModel.DepartmentId);
            return View(viewModel);
        }

        // GET: Professors/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = await db.Professors.FindAsync(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", professor.DepartmentId);
            return View(new ProfessorEditViewModel(professor));
        }

        // POST: Professors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(ProfessorEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var professor = new Professor(viewModel);
                db.Entry(professor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", viewModel.DepartmentId);
            return View(viewModel);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Professor professor = await db.Professors.FindAsync(id);
            db.Users.Remove(professor);
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
