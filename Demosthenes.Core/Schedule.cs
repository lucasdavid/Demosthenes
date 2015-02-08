using Demosthenes.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Core
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Index("SCHEDULE_UNIQ_IX", IsUnique = true, Order = 0)]
        public TimeSpan TimeStarted  { get; set; }

        [Index("SCHEDULE_UNIQ_IX", IsUnique = true, Order = 1)]
        public TimeSpan TimeFinished { get; set; }

        public virtual ICollection<ClassSchedule> Classes { get; set; }
    }
}
