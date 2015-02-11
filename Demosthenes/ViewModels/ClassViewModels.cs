using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Demosthenes.Core;

namespace Demosthenes.ViewModels
{
    public class ClassCreateViewModel
    {
        [Required]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Professor")]
        public string ProfessorId { get; set; }

        [Required]
        public bool Enrollable { get; set; }

        [Required]
        [Display(Name = "Class's size")]
        public int Size { get; set; }
    }

    public class ClassUpdateViewModel : ClassCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }

    public class ClassScheduleViewModel
    {
        [Required]
        [Display(Name = "Class")]
        public int ClassId { get; set; }

        [Required]
        [Display(Name = "Schedule")]
        public int ScheduleId { get; set; }

        [Required]
        [Display(Name = "Day of week")]
        public DayOfWeek DayOfWeek { get; set; }
    }

    public class ClassResultViewModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string ProfessorId { get; set; }
        public bool Enrollable { get; set; }
        public int Size { get; set; }
        public CourseResultViewModel Course { get; set; }
        public ProfessorResultViewModel Professor { get; set; }
        public ICollection<ClassScheduleResultViewModel> ClassSchedules { get; set; }
        public ICollection<EnrollmentResultViewModel> Enrollments { get; set; }

        static public implicit operator ClassResultViewModel(Class c)
        {
            return new ClassResultViewModel
            {
                Id             = c.Id,
                ProfessorId    = c.ProfessorId,
                Professor      = c.Professor,
                CourseId       = c.Id,
                Enrollable     = c.Enrollable,
                Size           = c.Size,
                Course         = c.Course,
                ClassSchedules = c.ClassSchedules.Select(cs => (ClassScheduleResultViewModel)cs).ToList(),
                Enrollments    = c.Enrollments.Select(e => (EnrollmentResultViewModel)e).ToList()
            };
        }
    }

    public class ClassScheduleResultViewModel
    {
        public int ScheduleId { get; set; }
        public virtual ScheduleResultViewModel Schedule { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        static public implicit operator ClassScheduleResultViewModel(ClassSchedule cs)
        {
            return new ClassScheduleResultViewModel
            {
                ScheduleId = cs.ScheduleId,
                Schedule   = cs.Schedule,
                DayOfWeek  = cs.DayOfWeek
            };
        }
    }
}