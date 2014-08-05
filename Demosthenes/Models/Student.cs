using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demosthenes.Models
{
    public class Student : ApplicationUser // Base.TimeStampsEntity
    {
        [Display(Name = "StudentName", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public string Name { get; set; }

        [Display(Name = "StudentDateBorn", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public DateTime DateBorn { get; set; }

        [Display(Name = "StudentClasses", ResourceType = typeof(Resources.i18n.Models))]
        public ICollection<Class> Classes { get; set; }
    }
}