using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Demosthenes.Core;
using Demosthenes.Core.Enums;

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

        [Required]
        public Term Term { get; set; }

        [Required]
        public int Year { get; set; }
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
    }

    public class ClassResultViewModel
    {
        public int    Id { get; set; }
        public int    CourseId    { get; set; }
        public string ProfessorId { get; set; }
        public bool   Enrollable  { get; set; }
        public int    Size { get; set; }
        public Term   Term { get; set; }
        public int    Year { get; set; }
        public CourseResultViewModel    Course    { get; set; }
        public ProfessorResultViewModel Professor { get; set; }
        public ICollection<ScheduleResultViewModel>   Schedules   { get; set; }
        public ICollection<EnrollmentResultViewModel> Enrollments { get; set; }

        static public implicit operator ClassResultViewModel(Class c)
        {
            return new ClassResultViewModel
            {
                Id          = c.Id,
                ProfessorId = c.ProfessorId,
                Professor   = c.Professor,
                CourseId    = c.CourseId,
                Course      = c.Course,
                Enrollable  = c.Enrollable,
                Size        = c.Size,
                Term        = c.Term,
                Year        = c.Year,
                Schedules   = c.Schedules.Select(s => (ScheduleResultViewModel)s).ToList(),
                Enrollments = c.Enrollments.Select(e => (EnrollmentResultViewModel)e).ToList()
            };
        }
    }
}