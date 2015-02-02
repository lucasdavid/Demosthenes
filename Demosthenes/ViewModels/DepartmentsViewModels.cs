using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demosthenes.ViewModels
{
    public class DepartmentNameViewModel
    {
        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Name { get; set; }
    }

    public class UpdateDepartmentViewModel : DepartmentNameViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}