using Demosthenes.Core;
using Demosthenes.Data;
using Demosthenes.Services.Infrastructure;

namespace Demosthenes.Services
{
    public class DepartmentService : Service<Department>
    {
        public DepartmentService(DemosthenesContext db) : base(db) { }
    }
}
