using Demosthenes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demosthenes.ViewModels
{
    public class StudentResultViewModel : UserResultViewModel
    {
        public ICollection<EnrollmentResultViewModel> Enrollments { get; set; }
        
        static public implicit operator StudentResultViewModel(Student s)
        {
            return new StudentResultViewModel
            {
                Id          = s.Id,
                Email       = s.Email,
                Name        = s.Name,
                PhoneNumber = s.PhoneNumber,
                Enrollments = s.Enrollments.Select(e => (EnrollmentResultViewModel)e).ToList()
            };
        }
    }
}