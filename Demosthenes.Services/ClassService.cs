using Demosthenes.Core;
using Demosthenes.Data;
using Demosthenes.Services.Exceptions;
using Demosthenes.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Demosthenes.Services
{
    public class ClassService : Service<Class>
    {
        public ClassService(DemosthenesContext db) : base(db) { }

        public async Task<ICollection<ClassSchedule>> ClassSchedulesOf(int classId)
        {
            return await Db.ClassSchedules
                .Where(e => e.ClassId == classId)
                .ToListAsync();
        }

        public async Task<int> ScheduleClass(int classId, int scheduleId, DayOfWeek dayOfWeek)
        {
            return await ScheduleClass(new ClassSchedule
            {
                ClassId = classId,
                ScheduleId = scheduleId,
                DayOfWeek = dayOfWeek
            });
        }

        public async Task<int> ScheduleClass(ClassSchedule cs)
        {
            var @class = await FindClassOrHalt(cs.ClassId);           

            if (@class.ClassSchedules.Any(e => e.ClassId == cs.ClassId && e.ScheduleId == cs.ScheduleId && e.DayOfWeek == cs.DayOfWeek))
            {
                throw new ClassAlreadyHasGivenScheduleException("Class {" + @class.Id
                    + "}, schedule {" + cs.ScheduleId
                    + "}, day {" + cs.DayOfWeek + "}");
            }

            @class.ClassSchedules.Add(cs);
            return await Update(@class);
        }

        public async Task<int> UnscheduleClass(ClassSchedule cs)
        {
            var @class = await FindClassOrHalt(cs.ClassId);
            cs = @class
                .ClassSchedules
                .FirstOrDefault(e => e.ClassId == cs.ClassId && e.ScheduleId == cs.ScheduleId && e.DayOfWeek == cs.DayOfWeek);

            if (cs == null)
            {
                throw new ClassDoesntHaveGivenScheduleException("Class {" + @class.Id
                    + "}, schedule {" + cs.ScheduleId
                    + "}, day {" + cs.DayOfWeek + "}");
            }

            @class.ClassSchedules.Remove(cs);
            return await Update(@class);
        }

        /// <summary>
        /// Searches for a Class of id equals to <paramref name="id"/>. If does not found it, throws KeyNotFoundException.
        /// </summary>
        /// <param name="id">Id of the searched class.</param>
        /// <returns></returns>
        protected async Task<Class> FindClassOrHalt(int id)
        {
            var @class = await Find(id);
            if (@class == null)
            {
                throw new KeyNotFoundException("Class {" + id + "} does not exit.");
            }

            return @class;
        }
    }
}
