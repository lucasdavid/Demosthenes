using Demosthenes.Core.Models;
using Demosthenes.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Demosthenes.Controllers
{
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
        public async Task<ActionResult> Index()
        {
            var professors = await db.Professors.Include("Department").ToListAsync();
            var viewModel = new List<ProfessorViewModel>();

            foreach (Professor professor in professors)
            {
                viewModel.Add(new ProfessorViewModel(professor));
            }

            return View(viewModel);
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
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            return View();
        }

        // POST: Professors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
