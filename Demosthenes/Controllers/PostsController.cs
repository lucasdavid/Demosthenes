using Demosthenes.Core.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;

namespace Demosthenes.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index(string q = null, int page = 1, int size = 10)
        {
            var id = User.Identity.GetUserId();

            var posts = db.Posts
                .Where(p => (p.Visible || p.AuthorId == id) && (q == null || p.Title.Contains(q) || p.Body.Contains(q)))
                .OrderByDescending(p => p.DateCreated)
                .Include(p => p.Author);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PostsList", posts.ToPagedList(page, size));
            }

            return View(posts.ToPagedList(page, size));
        }

        // GET: Posts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (!post.Visible && post.AuthorId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize(Roles = "admin,professor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,professor")]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Body")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.AuthorId = User.Identity.GetUserId();

                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "admin,professor")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            if (post.AuthorId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            return View(post);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,professor")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Body,AuthorId")] Post post)
        {
            if (post.AuthorId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,professor")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await db.Posts.FindAsync(id);

            if (!User.IsInRole("admin") && post.AuthorId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // TODO: implement this in the view.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,professor")]
        public async Task<ActionResult> Hide(int id)
        {
            var post = await db.Posts.FindAsync(id);

            if (!User.IsInRole("admin") && post.AuthorId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            post.Visible = false;
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = post.Id });
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
