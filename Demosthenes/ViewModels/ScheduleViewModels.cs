using Demosthenes.Core;
using Demosthenes.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demosthenes.ViewModels
{
    public class ScheduleCreateViewModel
    {
        [Required]
        [Display(Name = "Time started")]
        [TimeEqualsOrSmallerThanAttribute("TimeFinished", ErrorMessage="Starting time must be set before finishing time.")]
        public TimeSpan TimeStarted  { get; set; }
        
        [Required]
        [Display(Name="Time finished")]
        public TimeSpan TimeFinished { get; set; }

        [Required]
        [Display(Name = "Day of week")]
        public DayOfWeek DayOfWeek { get; set; }
    }

    public class ScheduleUpdateViewModel : ScheduleCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }

    public class ScheduleResultViewModel
    {
        public int Id { get; set; }
        public TimeSpan TimeStarted { get; set; }
        public TimeSpan TimeFinished { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        static public implicit operator ScheduleResultViewModel(Schedule s)
        {
            return new ScheduleResultViewModel
            {
                Id = s.Id,
                TimeStarted = s.TimeStarted,
                TimeFinished = s.TimeFinished,
                DayOfWeek = s.DayOfWeek
            };
        }
    }
}