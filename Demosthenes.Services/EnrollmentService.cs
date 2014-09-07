using Demosthenes.Core.Exceptions.Enrollment;
using Demosthenes.Core.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Demosthenes.Services
{
    public class EnrollmentService : Service<Enrollment>
    {
        public EnrollmentService(ApplicationDbContext db) : base(db) { }

        public async Task<Enrollment> Enroll(Student student, Class @class)
        {
            if (!@class.Enrollable)
            {
                throw new NonEnrollableClassException();
            }

            if (@class.Enrollment.Count == @class.Size)
            {
                throw new EnrollmentLimitOverflowException();
            }

            if (@class.Enrollment.Any(e => e.StudentId == student.Id))
            {
                throw new StudentAlreadyEnrolledException();
            }

            if (student.Enrollment.Any(e =>
                e.Class.Term == @class.Term && e.Class.Year == @class.Year
                && e.Class.Schedules.Any(a =>
                    @class.Schedules.Any(b =>
                        a.Day == b.Day && a.Starting < b.Ending && a.Ending > b.Starting))))
            {
                throw new ScheduleConflictException();
            }

            var enrollment = new Enrollment
            {
                Student = student,
                Class = @class
            };

            await AddAsync(enrollment);
            
            return enrollment;
        }

        public async Task<Enrollment> Unenroll(Student student, int enrollmentId)
        {
            var enrollment = await FindAsync(enrollmentId);

            if (enrollment.StudentId != student.Id)
            {
                throw new UnknownStudentException();
            }

            if (!enrollment.Class.Enrollable)
            {
                throw new NonEnrollableClassException();
            }

            await DeleteAsync(enrollment);
            return enrollment;
        }
    }
}
