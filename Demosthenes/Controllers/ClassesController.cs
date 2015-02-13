using Demosthenes.Core;
using Demosthenes.Services;
using Demosthenes.Services.Exceptions;
using Demosthenes.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Linq;

namespace Demosthenes.Controllers
{
    public class ClassesController : ApiController
    {
        private ClassService _classes;

        public ClassesController(ClassService classes)
        {
            _classes = classes;
        }

        // GET: api/Classes
        public async Task<ICollection<ClassResultViewModel>> GetClasses()
        {
            return (await _classes.All())
                .Select(c => (ClassResultViewModel)c)
                .ToList();
        }

        // GET: api/Classes/5
        [ResponseType(typeof(Class))]
        public async Task<IHttpActionResult> GetClass(int id)
        {
            Class @class = await _classes.Find(id);
            if (@class == null)
            {
                return NotFound();
            }

            return Ok((ClassResultViewModel)@class);
        }

        // GET: api/Classes/Courses/5
        [Route("api/Classes/Courses/{id}")]
        public async Task<ICollection<ClassResultViewModel>> GetClassesOfCourse(int id)
        {
            return (await _classes.OfCourse(id))
                .Select(c => (ClassResultViewModel)c)
                .ToList();
        }

        // PUT: api/Classes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClass(ClassUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    var @class = await _classes.Find(model.Id);
                    @class.CourseId = model.CourseId;
                    @class.ProfessorId = model.ProfessorId;
                    @class.Enrollable = model.Enrollable;
                    @class.Size = model.Size;

                    await _classes.Update(@class);
                    return StatusCode(HttpStatusCode.NoContent);
                }
                catch (NullReferenceException)
                {
                    ModelState.AddModelError("NullReferenceException", "Class {" + model.Id + "} doesn't exist.");
                }
            }

            return BadRequest(ModelState);
        }

        // POST: api/Classes
        [ResponseType(typeof(Class))]
        public async Task<IHttpActionResult> PostClass(ClassCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var c = new Class
                {
                    CourseId = model.CourseId,
                    ProfessorId = model.ProfessorId,
                    Enrollable = model.Enrollable,
                    Size = model.Size,
                    Term = model.Term,
                    Year = model.Year
                };

                try
                {
                    await _classes.Add(c);
                    return CreatedAtRoute("DefaultApi", new { id = c.Id }, (ClassResultViewModel)c);
                }
                catch (Exception)
                {
                    //
                }
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Classes/5
        [ResponseType(typeof(Class))]
        public async Task<IHttpActionResult> DeleteClass(int id)
        {
            Class @class = await _classes.Find(id);
            if (@class == null)
            {
                return NotFound();
            }

            await _classes.Delete(@class);
            return Ok((ClassResultViewModel)@class);
        }

        // GET: api/Classes/5/Schedules
        [Route("api/Classes/{classId}/Schedules")]
        public async Task<ICollection<ScheduleResultViewModel>> GetSchedules(int classId)
        {
            return (await _classes.SchedulesOf(classId))
                .Select(cs => (ScheduleResultViewModel)cs)
                .ToList();
        }

        // POST: api/Classes/5/Schedules/5
        [Route("api/Classes/{classId}/Schedules/{scheduleId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostScheduleClass(ClassScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _classes.ScheduleClass(model.ClassId, model.ScheduleId);
                    return Ok();
                }
                catch (Exception)
                {
                    //
                }
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Classes/5/Schedules/5
        [Route("api/Classes/{classId}/Schedules/{scheduleId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeleteUnscheduleClass(int classId, int scheduleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _classes.UnscheduleClass(classId, scheduleId);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _classes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}