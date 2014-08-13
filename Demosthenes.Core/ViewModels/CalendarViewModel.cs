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
        protected Dictionary<TimeSpan, Class>[] ClassesOnDay { get; set; }

        public CalendarViewModel()
        {
            ClassesOnDay    = new Dictionary<TimeSpan, Class>[7];

            for (int i = 0; i < 7; i++)
            {
                ClassesOnDay[i] = new Dictionary<TimeSpan, Class>();
            }
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
            foreach (Schedule schedule in @class.Schedules)
            {
                ClassesOnDay[(int)schedule.Day].Add(schedule.Starting, @class);
            }
        }

        public List<Class> GetClassesOnDay(DayOfWeek day)
        {
            return ClassesOnDay[(int)day].Values.ToList();
        }

        public List<Class> ClassesAt(TimeSpan time)
        {
            var classes = new List<Class>();

            foreach (var current in ClassesOnDay)
            {
                if (current.ContainsKey(time))
                {
                    classes.Add(current[time]);
                }
            }

            return classes;
        }

        public Class Class(DayOfWeek day, TimeSpan starting)
        {
            if (ClassesOnDay[(int)day].ContainsKey(starting))
            {
                return ClassesOnDay[(int)day][starting];
            }

            return null;
        }

        public List<TimeSpan> StartingTimes(DayOfWeek day)
        {
            return ClassesOnDay[(int)day].Keys.ToList();
        }
    }
}
