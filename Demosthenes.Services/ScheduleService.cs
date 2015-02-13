using Demosthenes.Core;
using Demosthenes.Data;
using Demosthenes.Services.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Demosthenes.Services
{
    public class ScheduleService : Service<Schedule>
    {
        public ScheduleService(DemosthenesContext db) : base(db) { }

        public async Task<ICollection<Schedule>> AllOrdered()
        {
            return await Db.Schedules
                .OrderBy(s => s.DayOfWeek)
                .ThenBy(s => s.TimeStarted)
                .ToListAsync();
        }
    }
}
