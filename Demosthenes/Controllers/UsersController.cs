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
using Demosthenes.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace Demosthenes.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private RoleManager<IdentityRole> RoleManager { get; set; }
        private ApplicationDbContext db { get; set; }
        private UserManager<ApplicationUser> UserManager { get; set; }

        public UsersController()
        {
            db = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        }

        public UsersController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            db = dbContext;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        // GET: ApplicationUsers
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            var id = RoleManager.FindByName("admin").Id;
            var applicationUsers = await db.Users
                .Where(user => user.Roles.Any(role => role.RoleId == id)
                    && (q == null || user.Name.Contains(q) || user.Email.Contains(q)))
                .ToArrayAsync();

            var models = new List<ApplicationUserViewModel>();

            foreach (ApplicationUser user in applicationUsers)
            {
                models.Add(new ApplicationUserViewModel(user));
            }

            ViewBag.q = q;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", models.ToPagedList(page, size));
            }

            return View(models.ToPagedList(page, size));
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(new ApplicationUserViewModel(applicationUser));
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email, Name = model.Name };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "admin");
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                AddErrors(result);
            }

            return View(model);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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
