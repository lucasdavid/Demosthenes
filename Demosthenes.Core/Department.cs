using Demosthenes.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Core
{
    public class Department : IDateTrackable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public string Lead { get; set; }

        public virtual ICollection<Professor> Professors { get; set; }
        public virtual ICollection<Course>    Courses    { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
