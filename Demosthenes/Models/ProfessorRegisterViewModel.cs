using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Models.ViewModels
{
    public class ProfessorRegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "ProfessorName", ResourceType = typeof(Resources.i18n.Models))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "ProfessorSSN", ResourceType = typeof(Resources.i18n.Models))]
        public string SSN { get; set; }

        [Display(Name = "ProfessorDepartmentId", ResourceType = typeof(Resources.i18n.Models))]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}