using Demosthenes.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Core.ViewModels
{
    public class ProfessorEditViewModel
    {
        public ProfessorEditViewModel() { }

        public ProfessorEditViewModel(Professor professor)
        {
            Id = professor.Id;
            Email = professor.Email;
            EmailConfirmed = professor.EmailConfirmed;
            Name = professor.Name;
            PhoneNumber = professor.PhoneNumber;
            PhoneNumberConfirmed = professor.PhoneNumberConfirmed;
            SSN = professor.SSN;
            DepartmentId = professor.DepartmentId;
            Department = professor.Department;
        }

        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "EmailConfirmed", ResourceType = typeof(Resources.i18n.Models))]
        public bool EmailConfirmed { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(Resources.i18n.Models))]
        public string Name { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.i18n.Models))]
        public string PhoneNumber { get; set; }

        [Display(Name = "PhoneNumberConfirmed", ResourceType = typeof(Resources.i18n.Models))]
        public bool PhoneNumberConfirmed { get; set; }

        [Required]
        [Display(Name = "SSN", ResourceType = typeof(Resources.i18n.Models))]
        public string SSN { get; set; }

        [Display(Name = "DepartmentId", ResourceType = typeof(Resources.i18n.Models))]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}