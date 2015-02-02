using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demosthenes.ViewModels
{
    public class ProfessorRegisterViewModel : RegisterBindingModel
    {
        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
    }
}
