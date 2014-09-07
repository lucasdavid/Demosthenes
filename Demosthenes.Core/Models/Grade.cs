using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Core.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        [Required]
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Required]
        [Range(0, 1000)]
        public uint Score { get; set; }
    }
}
