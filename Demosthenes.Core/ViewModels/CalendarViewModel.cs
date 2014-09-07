using Demosthenes.Core.Models;
using System;
using System.Collections.Generic;

namespace Demosthenes.Core.ViewModels
{
    public class CalendarViewModel
    {
        private List<Class> classes;
        private List<TimeSpan> times;
        private string[] days;
        private int year;
        private string term;

        public CalendarViewModel(List<Class> classes, List<TimeSpan> times, string[] days, int year, string term)
        {
            this.classes = classes;
            this.times = times;
            this.days = days;
            this.year = year;
            this.term = term;
        }
    }
}
