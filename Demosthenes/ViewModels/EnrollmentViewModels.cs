using Demosthenes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demosthenes.ViewModels
{
    public class EnrollmentResultViewModel
    {
        public int       Id { get; set; }
        public string    StudentId { get; set; }
        public int       ClassId { get; set; }
        public DateTime  DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        static public implicit operator EnrollmentResultViewModel(Enrollment e)
        {
            return new EnrollmentResultViewModel
            {
                Id          = e.Id,
                StudentId   = e.StudentId,
                ClassId     = e.ClassId,
                DateCreated = e.DateCreated,
                DateUpdated = e.DateUpdated
            };
        }
    }
}