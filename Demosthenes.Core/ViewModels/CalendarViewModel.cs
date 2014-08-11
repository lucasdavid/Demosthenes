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
        public List<Class> InsertedClasses { get; protected set; }
        public Dictionary<DayOfWeek, List<Class>> ClassesOnDay { get; protected set; }

        public CalendarViewModel()
        {
            InsertedClasses = new List<Class>();
            ClassesOnDay = new Dictionary<DayOfWeek, List<Class>>();
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
            if (InsertedClasses.Contains(@class))
            {
                throw new Exception("The class " + @class.Id + " was already inserted into the calendar");
            }

            InsertedClasses.Add(@class);

            foreach (Schedule schedule in @class.Schedules)
            {
                GetClassesOnDay(schedule.Day).Add(@class);
            }
        }

        public List<Class> GetClassesOnDay(DayOfWeek day)
        {
            List<Class> classes;
            if (ClassesOnDay.ContainsKey(day))
            {
                classes = ClassesOnDay[day];
            }
            else
            {
                classes = ClassesOnDay[day] = new List<Class>();
            }

            return classes;
        }

        public Class GetClass(DayOfWeek day, TimeSpan time)
        {
            var classes = ClassesOnDay[day];

            foreach (Class @class in classes)
            {
                // if (@class.Schedules)
            }

            return null;
        }
    }
}
