using Demosthenes.Core;
using Demosthenes.Data;
using Demosthenes.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Services
{
    public class DepartmentService : Service<Department>
    {
        public DepartmentService(DemosthenesContext db) : base(db) { }
    }
}
