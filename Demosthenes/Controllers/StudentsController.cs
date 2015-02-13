using Demosthenes.Core;
using Demosthenes.Services;
using Demosthenes.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;

namespace Demosthenes.Controllers
{
    public class StudentsController : ApiController
    {
        private ApplicationUserManager _userManager;
        private StudentService         _students;

        public StudentsController(ApplicationUserManager userManager, StudentService students)
        {
            UserManager = userManager;
            _students = students;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: api/Students
        public async Task<ICollection<StudentResultViewModel>> GetStudents()
        {
            return (await _students.All())
                .Select(s => (StudentResultViewModel)s)
                .ToList();
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> GetStudent(string id)
        {
            Student student = await _students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok((StudentResultViewModel)student);
        }

        // PUT: api/Students/5
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStudent(UpdateBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var student = await _students.Find(model.Id);
                student.Email       = student.UserName = model.Email;
                student.Name        = model.Name;
                student.PhoneNumber = model.PhoneNumber;

                await _students.Update(student);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_students.Exists(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Students
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> PostStudent(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = new Student
            {
                UserName    = model.Email,
                Email       = model.Email,
                Name        = model.Name,
                PhoneNumber = model.PhoneNumber,
            };

            IdentityResult result = await UserManager.CreateAsync(student, model.Password);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return CreatedAtRoute("DefaultApi", new { id = student.Id }, (StudentResultViewModel)student);
        }

        // DELETE: api/Students/5
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> DeleteStudent(string id)
        {
            Student student = await _students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            await _students.Delete(student);
            return Ok((StudentResultViewModel)student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _students.Dispose();
            }
            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}