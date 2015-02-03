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
        [Display(Name = "Social Security Number")]
        [RegularExpression(@"\d{3}-\d{3}-\d{4}",
            ErrorMessage = "Social Security Number must be informed as the following patter: ddd-ddd-dddd")]
        public string SSN { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
    }

    public class ProfessorUpdateViewModel : UpdateBindingModel
    {
        [Required]
        [Display(Name = "Social Security Number")]
        [RegularExpression(@"\d{3}-\d{3}-\d{4}")]
        public string SSN { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
    }
}
