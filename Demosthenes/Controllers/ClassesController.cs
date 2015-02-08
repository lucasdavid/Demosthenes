using Demosthenes.Core;
using Demosthenes.Services;
using Demosthenes.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

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
        public async Task<ICollection<Class>> GetClasses()
        {
            return await _classes.All();
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

            return Ok(@class);
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

            return CreatedAtRoute("DefaultApi", new { id = @class.Id }, @class);
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
            return Ok(@class);
        }

        // GET: api/Classes/5/Schedules
        [Route("api/Classes/{classId}/Schedules")]
        public async Task<ICollection<ClassSchedule>> GetClassSchedules(int classId)
        {
            return await _classes.ClassSchedulesOf(classId);
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