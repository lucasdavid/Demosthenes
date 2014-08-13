using Demosthenes.Core.Models;
using System;

namespace Demosthenes.Core.ViewModels
{
    public class ScheduleJsonViewModel
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan Starting { get; set; }
        public TimeSpan Ending { get; set; }

        public ScheduleJsonViewModel() { }
        public ScheduleJsonViewModel(Schedule schedule)
        {
            Id = schedule.Id;
            Day = schedule.Day;
            Starting = schedule.Starting;
            Ending = schedule.Ending;
        }
    }
}
