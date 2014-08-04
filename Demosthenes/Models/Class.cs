using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demosthenes.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Required, ForeignKey("Professor")]
        public int ProfessorId { get; set; }
        public Course Professor { get; set; }

        [Required] public short Size { get; set; }
        [Required] public int   Year { get; set; }
        [Required] public Term  Term { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}