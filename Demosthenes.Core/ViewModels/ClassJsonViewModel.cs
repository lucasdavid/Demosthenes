using Demosthenes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Core.ViewModels
{
    public class ClassJsonViewModel
    {
        public int Id { get; set; }
        public CourseJsonViewModel Course { get; set; }
        public List<ScheduleJsonViewModel> Schedules { get; set; }

        public ClassJsonViewModel() { }
        
        public ClassJsonViewModel(Class @class)
        {
            Id = @class.Id;
            Course = new CourseJsonViewModel(@class.Course);
            Schedules = new List<ScheduleJsonViewModel>();
            foreach (var schedule in @class.Schedules)
            {
                Schedules.Add(new ScheduleJsonViewModel(schedule));
            }
        }
    }
}
