using Demosthenes.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Core.ViewModels
{
    public class StudentEditViewModel
    {
        public StudentEditViewModel() { }

        public StudentEditViewModel(Student student)
        {
            Id = student.Id;
            Email = student.Email;
            EmailConfirmed = student.EmailConfirmed;
            Name = student.Name;
            PhoneNumber = student.PhoneNumber;
            PhoneNumberConfirmed = student.PhoneNumberConfirmed;
            DateBorn = student.DateBorn;
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

        [Display(Name = "DateBorn", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public DateTime DateBorn { get; set; }
    }
}
