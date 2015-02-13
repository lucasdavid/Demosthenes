using Demosthenes.Core;
using Demosthenes.Services;
using Demosthenes.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Demosthenes.Controllers
{
    public class DepartmentsController : ApiController
    {
        private DepartmentService _departments;

        public DepartmentsController(DepartmentService departments)
        {
            _departments = departments;
        }

        // GET: api/Departments
        public async Task<ICollection<DepartmentResultViewModel>> GetDepartments()
        {
            return (await _departments.All())
                .Select(e => (DepartmentResultViewModel) e)
                .ToList();
        }

        // GET: api/Departments/5
        [ResponseType(typeof(Department))]
        public async Task<IHttpActionResult> GetDepartment(int id)
        {
            var department = (DepartmentResultViewModel) await _departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // PUT: api/Departments/5
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDepartment(DepartmentUpdateViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = await _departments.Find(viewmodel.Id);
            if (department == null)
            {
                return NotFound();
            }

            department.Name = viewmodel.Name;
            department.Lead = viewmodel.Lead;

            try
            {
                await _departments.Update(department);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Departments
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(Department))]
        public async Task<IHttpActionResult> PostDepartment(DepartmentCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = await _departments.Add(new Department
            {
                Name = model.Name
            });

            try {
                var result = (DepartmentResultViewModel) department;
                return CreatedAtRoute("DefaultApi", new { id = department.Id }, result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Departments/5
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(Department))]
        public async Task<IHttpActionResult> DeleteDepartment(int id)
        {
            try
            {
                await _departments.Delete(id);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _departments.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}