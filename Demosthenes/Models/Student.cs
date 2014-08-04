using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demosthenes.Models
{
    public class Student
    {
        public Student()
        {
            DateCreated = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        //public int RA { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateBorn { get; set; }

        public DateTime DateCreated { get; protected set; }

        public ICollection<Class> Classes { get; set; }
    }
}