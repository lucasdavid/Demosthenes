using Demosthenes.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Core.ViewModels
{
    public class ClassSchedulesViewModel
    {
        public ClassSchedulesViewModel()
        { }

        public ClassSchedulesViewModel(Class @class)
            : this()
        {
            Id = @class.Id;
            Title = @class.Course.Title;

            Schedules = new HashSet<int>();
            foreach (Schedule schedule in @class.Schedules)
            {
                Schedules.Add(schedule.Id);
            }
        }

        [Required]
        public int Id { get; set; }

        public string Title { get; set; }

        [Required]
        [Display(Name = "ClassSchedules", ResourceType = typeof(Resources.i18n.Models))]
        public ICollection<int> Schedules { get; set; }
    }
}
