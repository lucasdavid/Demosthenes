using Demosthenes.Core;
using Demosthenes.Data;
using Demosthenes.Services.Infrastructure;

namespace Demosthenes.Services
{
    public class ProfessorService : Service<Professor>
    {
        public ProfessorService(DemosthenesContext db) : base(db) { }
    }
}
