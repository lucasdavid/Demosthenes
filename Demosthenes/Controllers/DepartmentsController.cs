using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Demosthenes.Core.Models;
using Demosthenes.Services;
using System.Data.SqlClient;

namespace Demosthenes.Controllers
{
    [Authorize(Roles = "admin")]
    public class DepartmentsController : Controller
    {
        private readonly DepartmentService service;

        public DepartmentsController(DepartmentService _service)
        {
            service = _service;
        }

        // GET: Departments
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            ViewBag.q = q;
            var occurrences = await service.SearchAsync(q);
            
            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", occurrences.ToPagedList(page, size));
            }
            return View(occurrences.ToPagedList(page, size));
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await service.AddAsync(department);
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await service.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                await service.UpdateAsync(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await service.DeleteAsync(id);
            }
            catch (SqlException)
            {
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
