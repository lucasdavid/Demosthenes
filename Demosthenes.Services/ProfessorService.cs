using Demosthenes.Core;
using Demosthenes.Data;
using Demosthenes.Services.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Demosthenes.Services
{
    public class ProfessorService : Service<Professor>
    {
        public ProfessorService(DemosthenesContext db) : base(db) { }

        public async Task<ICollection<Professor>> OfDepartment(int departmentId)
        {
            return await Db.Professors
                .Where(p => p.DepartmentId == departmentId)
                .ToListAsync();
        }
    }
}
