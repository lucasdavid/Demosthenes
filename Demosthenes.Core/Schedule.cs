using Demosthenes.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Core
{
    public class Schedule
    {
        public Schedule()
        {
            Classes = new List<Class>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Index("SCHEDULE_UNIQ_IX", IsUnique = true, Order = 1)]
        public TimeSpan TimeStarted { get; set; }

        [Required]
        [Index("SCHEDULE_UNIQ_IX", IsUnique = true, Order = 2)]
        public TimeSpan TimeFinished { get; set; }

        [Required]
        [Index("SCHEDULE_UNIQ_IX", IsUnique = true, Order = 3)]
        public DayOfWeek DayOfWeek { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
