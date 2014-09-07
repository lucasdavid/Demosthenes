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
using PagedList;
using Demosthenes.Services;

namespace Demosthenes.Controllers
{
    [Authorize(Roles = "admin")]
    public class CoursesController : Controller
    {
        private readonly CourseService courses;
        private readonly DepartmentService departments;

        public CoursesController(CourseService _courses, DepartmentService _departments)
        {
            courses = _courses;
            departments = _departments;
        }

        // GET: Courses
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            ViewBag.q = q;
            var occurrences = await courses.SearchAsync(q);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", occurrences.ToPagedList(page, size));
            }
            return View(occurrences.ToPagedList(page, size));
        }

        // GET: Courses/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.DepartmentId = new SelectList(await departments.AllAsync(), "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Details,DepartmentId")] Course course)
        {
            if (ModelState.IsValid)
            {
                await courses.AddAsync(course);
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(await departments.AllAsync(), "Id", "Name", course.DepartmentId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            ViewBag.DepartmentId = new SelectList(await departments.AllAsync(), "Id", "Name", course.DepartmentId);
            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Details,DepartmentId")] Course course)
        {
            if (ModelState.IsValid)
            {
                await courses.UpdateAsync(course);
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(await departments.AllAsync(), "Id", "Name", course.DepartmentId);
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Course course = await courses.FindAsync(id);
            await courses.DeleteAsync(course);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                courses.Dispose();
                departments.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
