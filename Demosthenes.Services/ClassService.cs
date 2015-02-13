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

        /// <summary>
        /// Retrieves all Classes of a given Course.
        /// </summary>
        /// <param name="courseId">The id of the Course of which Classes should be retrieved.</param>
        /// <returns>A collection of Classes.</returns>
        public async Task<ICollection<Class>> OfCourse(int courseId)
        {
            return await Db.Classes
                .Where(c => c.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<ICollection<Schedule>> SchedulesOf(int classId)
        {
            return await Db.Schedules
                .Where(s => s.Classes.Any(c => c.Id == classId))
                .ToListAsync();
        }

        public async Task<int> ScheduleClass(int classId, int scheduleId)
        {
            var c = await FindClassOrHalt(classId);
            var s = await FindScheduleOrHalt(scheduleId);

            return await ScheduleClass(c, s);
        }

        public async Task<int> ScheduleClass(Class c, Schedule s)
        {
            if (c.Schedules.Any(curr => curr.Id == s.Id))
            {
                throw new ClassAlreadyHasGivenScheduleException("{" + c.Id + "} -> {" + s.Id + "}");
            }

            c.Schedules.Add(s);
            return await Update(c);
        }

        public async Task<int> UnscheduleClass(int classId, int scheduleId)
        {
            var c = await FindClassOrHalt(classId);
            var s = await FindScheduleOrHalt(scheduleId);

            return await UnscheduleClass(c, s);
        }

        public async Task<int> UnscheduleClass(Class c, Schedule s)
        {
            var schedule = c.Schedules
                .Where(curr => curr.Id == s.Id)
                .FirstOrDefault();

            if (schedule == null)
            {
                throw new ClassDoesntHaveGivenScheduleException("Class {" + c.Id + "}, schedule {" + s.Id + "}");
            }

            c.Schedules.Remove(schedule);
            return await Update(c);
        }

        public async Task<int> Enroll(int classId, string studentId)
        {
            var @class = await FindClassOrHalt(classId);
            var student = await FindStudentOrHalt(studentId);

            if (@class.Enrollments.Any(e => e.StudentId == studentId))
            {
                throw new StudentAlreadyEnrolledException("{" + student.Id + " -> " + @class.Id + "}");
            }

            Db.Enrollments.Add(new Enrollment
            {
                ClassId = @class.Id,
                StudentId = student.Id
            });

            return await Db.SaveChangesAsync();
        }

        public async Task<int> Unenroll(int enrollmentId)
        {
            var enrollment = await Db.Enrollments.FindAsync(enrollmentId);

            if (enrollment == null)
            {
                throw new StudentNotEnrolledException("{" + enrollmentId + "}");
            }

            Db.Enrollments.Remove(enrollment);
            return await Db.SaveChangesAsync();
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
        protected async Task<Student> FindStudentOrHalt(string id)
        {
            var student = await Db.Students.FindAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException("Student {" + id + "} does not exit.");
            }

            return student;
        }
        protected async Task<Schedule> FindScheduleOrHalt(int id)
        {
            var s = await Db.Schedules.FindAsync(id);
            if (s == null)
            {
                throw new KeyNotFoundException("Student {" + id + "} does not exit.");
            }

            return s;
        }
    }
}
