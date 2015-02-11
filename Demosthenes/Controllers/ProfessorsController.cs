using Demosthenes.Core;
using Demosthenes.Services;
using Demosthenes.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Linq;

namespace Demosthenes.Controllers
{
    public class ProfessorsController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ProfessorService       _professors;

        public ProfessorsController(ApplicationUserManager userManager, ProfessorService professors)
        {
            UserManager = userManager;
            _professors = professors;
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

        // GET: api/Professors
        public async Task<ICollection<ProfessorResultViewModel>> GetProfessors()
        {
            return (await _professors.All())
                .Select(p => (ProfessorResultViewModel)p)
                .ToList();
        }

        // GET: api/Professors/5
        [ResponseType(typeof(Professor))]
        public async Task<IHttpActionResult> GetProfessor(string id)
        {
            var professor = await _professors.Find(id);
            if (professor == null)
            {
                return NotFound();
            }

            return Ok((ProfessorResultViewModel)professor);
        }

        // GET: api/Professors/Departments/5
        [Route("api/Professors/Departments/{id}")]
        public async Task<ICollection<ProfessorResultViewModel>> GetProfessorsOfDepartment(int id)
        {
            return (await _professors.OfDepartment(id))
                .Select(p => (ProfessorResultViewModel)p)
                .ToList();
        }
        
        // PUT: api/Professors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfessor(ProfessorUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var professor = await _professors.Find(model.Id);
                
                professor.SSN          = model.SSN;
                professor.Name         = model.Name;
                professor.UserName     = professor.Email = model.Email;
                professor.PhoneNumber  = model.PhoneNumber;
                professor.DepartmentId = model.DepartmentId;

                await _professors.Update(professor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_professors.Exists(model.Id))
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

        // POST: api/Professors
        [ResponseType(typeof(Professor))]
        public async Task<IHttpActionResult> PostProfessor(ProfessorRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var professor = new Professor
            {
                UserName     = model.Email,
                Email        = model.Email,
                Name         = model.Name,
                SSN          = model.SSN,
                PhoneNumber  = model.PhoneNumber,
                DepartmentId = model.DepartmentId
            };

            IdentityResult result = await UserManager.CreateAsync(professor, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return CreatedAtRoute("DefaultApi", new { id = professor.Id }, (ProfessorResultViewModel)professor);
        }

        // DELETE: api/Professors/5
        [ResponseType(typeof(Professor))]
        public async Task<IHttpActionResult> DeleteProfessor(string id)
        {
            Professor professor = await _professors.Find(id);
            if (professor == null)
            {
                return NotFound();
            }

            await _professors.Delete(professor);
            return Ok((ProfessorResultViewModel)professor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _professors.Dispose();
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