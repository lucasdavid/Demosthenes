using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Core
{
    public class Student : ApplicationUser
    {
        public Student()
        {
            Enrollments = new List<Enrollment>();
        }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
