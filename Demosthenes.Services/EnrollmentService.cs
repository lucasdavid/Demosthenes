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
    public class EnrollmentService : Service<Enrollment>
    {
        public EnrollmentService(DemosthenesContext db) : base(db) { }

        /// <summary>
        /// Enrolls student with Id <paramref name="studentId"/> to class of Id <paramref name="classId"/>.
        /// 
        /// Throws:
        ///     NonEnrollableClassException:
        ///         If one cannot enroll in class.
        ///     StudentAlreadyEnrolledException:
        ///         If student was already enrolled in class.
        /// </summary>
        /// <param name="studentId">The Id of the student to be enrolled.</param>
        /// <param name="classId">The ID of the class in which the student will be enrolled.</param>
        /// <returns>A task for the number of instances modifided.</returns>
        public async Task<int> Enroll(string studentId, int classId)
        {
            var operationDescription = "{" + studentId + " -> " + classId + "}";
            
            var c = await FindClassOrHalt(classId);
            var s = await FindStudentOrHalt(studentId);

            if (!c.Enrollable)
            {
                throw new NonEnrollableClassException(operationDescription);
            }

            if (c.Enrollments.Any(e => e.StudentId == studentId))
            {
                throw new StudentAlreadyEnrolledException(operationDescription);
            }

            if (c.Enrollments.Count == c.Size)
            {
                throw new EnrollmentLimitOverflowException(operationDescription);
            }

            if (s.Enrollments.Any(e =>
                e.Class.Term == c.Term && e.Class.Year == c.Year
                && e.Class.Schedules.Any(a =>
                    c.Schedules.Any(b =>
                        a.DayOfWeek == b.DayOfWeek
                        && a.TimeStarted  < b.TimeFinished
                        && a.TimeFinished > b.TimeStarted))))
            {
                throw new ScheduleConflictBetweenClassesException();
            }

            Db.Enrollments.Add(new Enrollment
            {
                ClassId = c.Id,
                StudentId = s.Id
            });

            return await Db.SaveChangesAsync();
        }

        public async Task<int> Unenroll(int enrollmentId)
        {
            var enrollment = await FindEnrollmentOrHalt(enrollmentId);

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
            var c = await Db.Classes.FindAsync(id);
            if (c == null)
            {
                throw new KeyNotFoundException("Class {" + id + "} does not exit.");
            }

            return c;
        }

        protected async Task<Student> FindStudentOrHalt(string id)
        {
            var s = await Db.Students.FindAsync(id);
            if (s == null)
            {
                throw new KeyNotFoundException("Student {" + id + "} does not exit.");
            }

            return s;
        }

        protected async Task<Enrollment> FindEnrollmentOrHalt(int id)
        {
            var e = await Find(id);
            if (e == null)
            {
                throw new KeyNotFoundException("Enrollment {" + id + "} does not exit.");
            }

            return e;
        }

        public async Task<ICollection<Enrollment>> OfStudent(string studentId)
        {
            return await Db.Enrollments
                .Where(e => e.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<ICollection<Enrollment>> OfClass(int classId)
        {
            return await Db.Enrollments
                .Where(e => e.ClassId == classId)
                .ToListAsync();
        }
    }
}
