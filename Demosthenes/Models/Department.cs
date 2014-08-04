using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Models
{
    public class Department
    {
        public Department()
        {
            DateCreated = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Adviser")]
        public int? AdviserId { get; set; }
        public Professor Adviser { get; set; }

        public virtual ICollection<Professor> Professors { get; set; }

        [Required]
        public DateTime DateCreated { get; protected set; }
    }
}
