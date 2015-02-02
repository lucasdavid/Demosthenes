using Demosthenes.Core;
using Demosthenes.Data;
using Demosthenes.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;

namespace Demosthenes.Controllers
{
    public class ProfessorsController : ApiController
    {
        private ApplicationUserManager _userManager;
        private DemosthenesContext db = new DemosthenesContext();

        public ProfessorsController() { }

        public ProfessorsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
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
        public async Task<ICollection<Professor>> GetProfessors()
        {
            return await db.Professors.ToListAsync();
        }

        // GET: api/Professors/5
        [ResponseType(typeof(Professor))]
        public async Task<IHttpActionResult> GetProfessor(string id)
        {
            Professor professor = await db.Professors.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }

            return Ok(professor);
        }

        // PUT: api/Professors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfessor(UpdateBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var professor = await db.Professors.FindAsync(model.Id);
                db.Entry(professor).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessorExists(model.Id))
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

            var professor = new Professor { UserName = model.Email, Email = model.Email, Name = model.Name, DepartmentId = model.DepartmentId };

            IdentityResult result = await UserManager.CreateAsync(professor, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return CreatedAtRoute("DefaultApi", new { id = professor.Id }, professor);
        }

        // DELETE: api/Professors/5
        [ResponseType(typeof(Professor))]
        public async Task<IHttpActionResult> DeleteProfessor(string id)
        {
            Professor professor = await db.Professors.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }

            db.Users.Remove(professor);
            await db.SaveChangesAsync();

            return Ok(professor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfessorExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
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