using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Core.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Required]
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
    }
}
