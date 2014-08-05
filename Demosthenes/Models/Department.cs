using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Models
{
    public class Department : Base.TimeStampsEntity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "DepartmentName", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public string Name { get; set; }

        [Display(Name = "DepartmentProfessors", ResourceType = typeof(Resources.i18n.Models))]
        public virtual ICollection<Professor> Professors { get; set; }
    }
}
