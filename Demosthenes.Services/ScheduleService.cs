using Demosthenes.Core;
using Demosthenes.Data;
using Demosthenes.Services.Infrastructure;

namespace Demosthenes.Services
{
    public class ScheduleService : Service<Schedule>
    {
        public ScheduleService(DemosthenesContext db) : base(db) { }
    }
}
