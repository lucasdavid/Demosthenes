using Demosthenes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Demosthenes.Services
{
    public class ScheduleService : Service<Schedule>
    {
        public ScheduleService(ApplicationDbContext db) : base(db) { }

        /// <summary>
        /// Group all schedules by starting time and return this collection of TimeSpans
        /// </summary>
        /// <returns>A collection with all possible starting hours</returns>
        public async Task<ICollection<TimeSpan>> AllStartingTimes()
        {
            return await db.Schedules
                .GroupBy(s => s.Starting)
                .Select(g => g.Key)
                .ToListAsync();
        }
    }
}
