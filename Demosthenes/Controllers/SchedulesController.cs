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
using Demosthenes.Services;
using Demosthenes.ViewModels;

namespace Demosthenes.Controllers
{
    public class SchedulesController : ApiController
    {
        private ScheduleService _schedules;

        public SchedulesController(ScheduleService schedules)
        {
            _schedules = schedules;
        }

        // GET: api/Schedules
        public async Task<ICollection<Schedule>> GetSchedules()
        {
            return await _schedules.All();
        }

        // GET: api/Schedules/5
        [ResponseType(typeof(Schedule))]
        public async Task<IHttpActionResult> GetSchedule(int id)
        {
            Schedule schedule = await _schedules.Find(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return Ok(schedule);
        }

        // PUT: api/Schedules/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSchedule(ScheduleUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var schedule = await _schedules.Find(model.Id);
                schedule.TimeStarted = model.TimeStarted;
                schedule.TimeFinished = model.TimeFinished;

                await _schedules.Update(schedule);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_schedules.Exists(model.Id))
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

        // POST: api/Schedules
        [ResponseType(typeof(Schedule))]
        public async Task<IHttpActionResult> PostSchedule(ScheduleCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schedule = await _schedules.Add(new Schedule
            {
                TimeStarted  = model.TimeStarted,
                TimeFinished = model.TimeFinished
            });

            return CreatedAtRoute("DefaultApi", new { id = schedule.Id }, schedule);
        }

        // DELETE: api/Schedules/5
        [ResponseType(typeof(Schedule))]
        public async Task<IHttpActionResult> DeleteSchedule(int id)
        {
            Schedule schedule = await _schedules.Find(id);
            if (schedule == null)
            {
                return NotFound();
            }

            await _schedules.Delete(schedule);
            return Ok(schedule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _schedules.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}