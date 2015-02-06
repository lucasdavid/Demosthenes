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

        public ICollection<ScheduleClasses> Schedules { get; set; }
    }
}