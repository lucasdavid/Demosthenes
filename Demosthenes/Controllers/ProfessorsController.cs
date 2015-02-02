using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Demosthenes.Core;
using Demosthenes.Data;

namespace Demosthenes.Controllers
{
    public class ProfessorsController : ApiController
    {
        private DemosthenesContext db = new DemosthenesContext();

        // GET: api/Professors
        public IQueryable<Professor> GetProfessors()
        {
            return db.Professors;
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
        public async Task<IHttpActionResult> PutProfessor(string id, Professor professor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != professor.Id)
            {
                return BadRequest();
            }

            db.Entry(professor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessorExists(id))
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
        public async Task<IHttpActionResult> PostProfessor(Professor professor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(professor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfessorExists(professor.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
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
    }
}