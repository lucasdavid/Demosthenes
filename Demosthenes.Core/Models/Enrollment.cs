using Demosthenes.Infrastructure.Exceptions.Enrollment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Core.Models
{
    public class Enrollment
    {
        public Enrollment() { }
        public Enrollment(Models.Student student, Models.Class @class)
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

            // TODO: unit-test this.
            if (student.Enrollment.Any(e =>
                e.Class.Term == @class.Term && e.Class.Year == @class.Year
                && e.Class.Schedules.Any(a =>
                    @class.Schedules.Any(b =>
                        a.Day == b.Day && a.Starting < b.Ending && a.Ending > b.Starting))))
            {
                throw new ScheduleConflictException();
            }

            Student = student;
            Class = @class;
        }

        [Key]
        [Column(Order = 0)]
        [Required]
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
    }
}
