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
}