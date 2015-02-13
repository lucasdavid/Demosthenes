using DataAnnotationsExtensions;
using Demosthenes.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Core
{
    public class Course : IDateTrackable
    {
        public Course()
        {
            Dependencies = new List<Course>();
            Dependents   = new List<Course>();
            Classes      = new List<Class>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        [Index(IsUnique = true)]
        public string Title { get; set; }

        [Required]
        [Min(0)]
        public short Credits { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Course> Dependencies { get; set; }
        public virtual ICollection<Course> Dependents { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public DateTime  DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
