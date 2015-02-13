using DataAnnotationsExtensions;
using Demosthenes.Core.Enums;
using Demosthenes.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Core
{
    public class Class : IDateTrackable
    {
        public Class()
        {
            Schedules   = new List<Schedule>();
            Enrollments = new List<Enrollment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Required]
        [ForeignKey("Professor")]
        public string ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }

        [Required]
        public bool Enrollable { get; set; }
        [Required]
        [Min(0)]
        public int Size { get; set; }

        [Required]
        public Term Term { get; set; }

        [Required]
        [Min(0)]
        public int Year { get; set; }

        public virtual ICollection<Schedule>   Schedules { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public DateTime  DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
