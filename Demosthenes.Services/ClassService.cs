using Demosthenes.Core;
using Demosthenes.Data;
using Demosthenes.Services.Infrastructure;

namespace Demosthenes.Services
{
    public class ClassService : Service<Class>
    {
        public ClassService(DemosthenesContext db) : base(db) { }
    }
}
