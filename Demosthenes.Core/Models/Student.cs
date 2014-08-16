using Demosthenes.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demosthenes.Core.Models
{
    public class Student : ApplicationUser
    {
        public Student()
        {
        }

        public Student(StudentViewModel viewModel) : this(viewModel as StudentEditViewModel)
        {
        }

        public Student(StudentEditViewModel viewModel)
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
            DateBorn = viewModel.DateBorn;
        }

        public DateTime DateBorn     { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}