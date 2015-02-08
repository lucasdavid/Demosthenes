using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Core
{
    [Table("Professors")]
    public class Professor : ApplicationUser
    {
        [Required]
        [RegularExpression(@"\d{3}-\d{3}-\d{4}")]
        public string SSN { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
