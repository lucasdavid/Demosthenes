using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Core
{
    public class ClassSchedule
    {
        [Key, Column(Order = 1)]
        [Required]
        [ForeignKey("Schedule")]
        [Index("SCHCLS_UNIQUE_IX", IsUnique = true, Order = 1)]
        public int ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }

        [Key, Column(Order = 2)]
        [Required]
        [ForeignKey("Class")]
        [Index("SCHCLS_UNIQUE_IX", IsUnique = true, Order = 2)]
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        [Key, Column(Order = 3)]
        [Required]
        [Index("SCHCLS_UNIQUE_IX", IsUnique = true, Order = 3)]
        public DayOfWeek DayOfWeek { get; set; }
    }
}
