using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Core.Models
{
    public class Professor : ApplicationUser
    {
        public Professor()
        {
        }

        public Professor(ViewModels.ProfessorViewModel viewModel)
            : this(viewModel as ViewModels.ProfessorEditViewModel)
        {
        }

        public Professor(ViewModels.ProfessorEditViewModel viewModel)
        {
            if (viewModel.Id != null)
            {
                Id = viewModel.Id;
            }

            UserName = viewModel.Email;
            Email = viewModel.Email;
            EmailConfirmed = viewModel.EmailConfirmed;
            PhoneNumber = viewModel.PhoneNumber;
            PhoneNumberConfirmed = viewModel.PhoneNumberConfirmed;

            Name = viewModel.Name;
            SSN = viewModel.SSN;

            DepartmentId = viewModel.DepartmentId;
        }

        public string SSN { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}