using Demosthenes.Core;
using Demosthenes.Data;
using Demosthenes.Services.Infrastructure;

namespace Demosthenes.Services
{
    public class StudentService : Service<Student>
    {
        public StudentService(DemosthenesContext db) : base(db) { }
    }
}
