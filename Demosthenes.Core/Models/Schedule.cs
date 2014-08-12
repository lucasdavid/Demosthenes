using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Core.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Day", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public DayOfWeek Day { get; set; }

        [Display(Name = "Starting", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public TimeSpan Starting { get; set; }

        [Display(Name = "Ending", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public TimeSpan Ending { get; set; }

        [Display(Name = "ScheduleClasses", ResourceType = typeof(Resources.i18n.Models))]
        public virtual ICollection<Class> Classes { get; set; }

        public override string ToString()
        {
            return Day.ToString() + ", from " + Starting.ToString(@"hh\:mm") + " to " + Ending.ToString(@"hh\:mm");
        }
    }
}
