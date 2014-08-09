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

            Schedules = new List<int>();
            foreach (Schedule schedule in @class.Schedules)
            {
                Schedules.Add(schedule.Id);
            }
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "ClassSchedules", ResourceType = typeof(Resources.i18n.Models))]
        public List<int> Schedules { get; set; }
    }
}
