using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Core.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "CourseId", ResourceType = typeof(Resources.i18n.Models))]
        [ForeignKey("Course")]
        public int? CourseId { get; set; }
        public Course Course { get; set; }

        [Display(Name = "ProfessorId", ResourceType = typeof(Resources.i18n.Models))]
        [ForeignKey("Professor")]
        public string ProfessorId { get; set; }
        public Professor Professor { get; set; }

        [Display(Name = "ClassSize", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public short Size { get; set; }

        [Display(Name = "Year", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public int Year { get; set; }

        [Display(Name = "Term", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public Term Term { get; set; }

        [Display(Name = "Enrollable", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public bool Enrollable { get; set; }

        [Display(Name = "ClassSchedules", ResourceType = typeof(Resources.i18n.Models))]
        public virtual ICollection<Schedule> Schedules { get; set; }

        [Display(Name = "ClassStudents", ResourceType = typeof(Resources.i18n.Models))]
        public virtual ICollection<Student> Students { get; set; }

        public bool Enroll(Student student)
        {
            if (Enrollable && Students.Count < Size)
            {
                Students.Add(student);
                return true;
            }

            return false;
        }
    }
}