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
}