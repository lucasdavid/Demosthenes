using Demosthenes.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demosthenes.ViewModels
{
    public class DepartmentCreateViewModel
    {
        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Name { get; set; }
    }

    public class DepartmentUpdateViewModel : DepartmentCreateViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Lead { get; set; }
    }

    public class DepartmentResultViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lead { get; set; }
        public int CoursesCount { get; set; }
        public int ProfessorsCount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        static public implicit operator DepartmentResultViewModel(Department d)
        {
            if (d == null)
            {
                return null;
            }

            return new DepartmentResultViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Lead = d.Lead,
                CoursesCount = d.Courses.Count(),
                ProfessorsCount = d.Professors.Count(),
                DateCreated = d.DateCreated,
                DateUpdated = d.DateUpdated
            };
        }
    }
}