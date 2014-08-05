using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Models
{
    public class Course : Base.TimeStampsEntity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "CourseTitle", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public string Title { get; set; }

        [Display(Name = "CourseDetails", ResourceType = typeof(Resources.i18n.Models))]
        public string Details { get; set; }

        [Display(Name = "CourseDepartmentId", ResourceType = typeof(Resources.i18n.Models))]
        [Required, ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Display(Name = "CourseClasses", ResourceType = typeof(Resources.i18n.Models))]
        public virtual ICollection<Class> Classes { get; set; }
    }
}