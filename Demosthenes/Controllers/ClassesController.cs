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
                return BadRequest(ModelState);
            }

            try
            {
                var @class = await _classes.Find(model.Id);
                @class.CourseId    = model.CourseId;
                @class.ProfessorId = model.ProfessorId;
                @class.Enrollable  = model.Enrollable;
                @class.Size        = model.Size;
                
                await _classes.Update(@class);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_classes.Exists(model.Id))
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

        // POST: api/Classes
        [ResponseType(typeof(Class))]
        public async Task<IHttpActionResult> PostClass(ClassCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var @class = new Class
            {
                CourseId    = model.CourseId,
                ProfessorId = model.ProfessorId,
                Enrollable  = model.Enrollable,
                Size        = model.Size
            };

            await _classes.Add(@class);
            return CreatedAtRoute("DefaultApi", new { id = @class.Id }, (ClassResultViewModel)@class);
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
        public async Task<ICollection<ClassScheduleResultViewModel>> GetClassSchedules(int classId)
        {
            return (await _classes.ClassSchedulesOf(classId))
                .Select(cs => (ClassScheduleResultViewModel)cs)
                .ToList();
        }

        // POST: api/Classes/5/Schedules/5
        [Route("api/Classes/{classId}/Schedules/{scheduleId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostScheduleClass(int classId, int scheduleId, ClassScheduleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (classId != model.ClassId || scheduleId != model.ScheduleId)
            {
                return BadRequest("Primary keys given conflict with keys recovered from the model");
            }

            await _classes.ScheduleClass(new ClassSchedule
            {
                ClassId = model.ClassId,
                ScheduleId = model.ScheduleId,
                DayOfWeek = model.DayOfWeek
            });

            return Ok();
        }

        // DELETE: api/Classes/5/Schedules/5
        [Route("api/Classes/{classId}/Schedules/{scheduleId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeleteUnscheduleClass(int classId, int scheduleId, DayOfWeek dayOfWeek)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _classes.UnscheduleClass(new ClassSchedule
            {
                ClassId    = classId,
                ScheduleId = scheduleId,
                DayOfWeek  = dayOfWeek
            });

            return Ok();
        }

        // GET: api/Classes/5/Student/5
        [Route("api/Classes/{classId}/Students/{studentId}")]
        public async Task<ICollection<Enrollment>> GetClassEnrollments(int enrollmentId)
        {
            return (await _classes.Find(enrollmentId)).Enrollments;
        }

        // POST: api/Classes/5/Student/5
        [Route("api/Classes/{classId}/Students/{studentId}")]
        public async Task<IHttpActionResult> PostEnroll(int classId, string studentId)
        {
            try
            {
                await _classes.Enroll(classId, studentId);
                return Ok();
            }
            catch (StudentAlreadyEnrolledException e)
            {
                ModelState.AddModelError("enrollment", "Student already enrolled in class " + e.Message + " .");
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Classes/5/Student/5
        [Route("api/Classes/{classId}/Students/{studentId}")]
        public async Task<IHttpActionResult> DeleteUnenroll(int classId, string studentId, int enrollmentId)
        {
            try
            {
                await _classes.Unenroll(enrollmentId);
                return Ok();
            }
            catch (StudentNotEnrolledException e)
            {
                ModelState.AddModelError("unenrollment", "Student is not yet enrolled in class " + e.Message + " .");
                return BadRequest(ModelState);
            }
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