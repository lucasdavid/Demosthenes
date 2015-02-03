using Demosthenes.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demosthenes.Core
{
    public class Department : IDateTrackable
    {
        [Key]
        public int Id { get; set; }
        
        [MinLength(2)]
        [MaxLength(255)]
        public string Name { get; set; }

        public string Lead { get; set; }

        public virtual ICollection<Professor> Professors { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public DateTime? DateDeleted { get; set; }
    }
}
