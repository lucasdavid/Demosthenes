using Demosthenes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Core.ViewModels
{
    public class CalendarViewModel
    {
        public List<Class> AllClasses { get; protected set; }
        public Dictionary<DayOfWeek, List<Class>> DaysOfWeek { get; protected set; }

        public CalendarViewModel()
        {
            AllClasses = new List<Class>();
            DaysOfWeek = new Dictionary<DayOfWeek, List<Class>>();
        }

        public CalendarViewModel(List<Class> classes)
            : this()
        {
            AddClass(classes);
        }

        public void AddClass(List<Class> classes)
        {
            foreach (Class @class in classes)
            {
                AddClass(@class);
            }
        }

        public void AddClass(Class @class)
        {
            AllClasses.Add(@class);

            foreach (Schedule schedule in @class.Schedules)
            {
                var classesOfDay = DaysOfWeek[schedule.Day] ?? new List<Class>();
                classesOfDay.Add(@class);
                DaysOfWeek[schedule.Day] = classesOfDay;
            }
        }

        public Class GetClass(DayOfWeek day, TimeSpan time)
        {
            var classes = DaysOfWeek[day];

            foreach (Class @class in classes)
            {
                // if (@class.Schedules)
            }

            return null;
        }
    }
}
