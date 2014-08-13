using Demosthenes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demosthenes.Core.ViewModels
{
    public class ScheduleJsonViewModel
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan Starting { get; set; }
        public TimeSpan Ending { get; set; }
    }
}
