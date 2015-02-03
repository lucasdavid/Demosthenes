using Demosthenes.Core;
using Demosthenes.Data;
using Demosthenes.Services.Infrastructure;

namespace Demosthenes.Services
{
    public class CourseService : Service<Course>
    {
        public CourseService(DemosthenesContext db) : base(db) { }
    }
}
