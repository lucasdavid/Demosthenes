using Demosthenes.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demosthenes.ViewModels
{
    public class CourseCreateViewModel
    {
        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        public short Credits { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
    }

    public class CourseUpdateViewModel : CourseCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }

    public class CourseResultViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public short Credits { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentResultViewModel Department { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        static public implicit operator CourseResultViewModel(Course c)
        {
            return new CourseResultViewModel
            {
                Id           = c.Id,
                Title        = c.Title,
                Credits      = c.Credits,
                DepartmentId = c.DepartmentId,
                Department   = c.Department,
                DateCreated  = c.DateCreated,
                DateUpdated  = c.DateUpdated
            };
        }
    }
}