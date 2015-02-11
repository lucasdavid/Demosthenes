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
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        static public implicit operator DepartmentResultViewModel(Department d)
        {
            return new DepartmentResultViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Lead = d.Lead,
                DateCreated = d.DateCreated,
                DateUpdated = d.DateUpdated
            };
        }
    }
}