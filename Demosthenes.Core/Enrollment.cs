using Demosthenes.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Core
{
    public class Enrollment : IDateTrackable
    {
        [Key]
        public int Id { get; set; }

        [Index("ENROLL_IX_UNIQUE", Order = 1, IsUnique = true)]
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Index("ENROLL_IX_UNIQUE", Order = 2, IsUnique = true)]
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
