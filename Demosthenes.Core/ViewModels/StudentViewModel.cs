using Demosthenes.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Demosthenes.Core.ViewModels
{
    public class StudentViewModel : StudentEditViewModel
    {
        public StudentViewModel() { }
        public StudentViewModel(Student student) : base(student) { }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.i18n.Models))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.i18n.Models))]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
