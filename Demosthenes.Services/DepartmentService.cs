using Demosthenes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Demosthenes.Services
{
    public class DepartmentService : Service<Department>
    {
        public DepartmentService(ApplicationDbContext db) : base(db) { }

        public async Task<ICollection<Department>> SearchAsync(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return await AllAsync();
            }

            return await db.Departments
                .Where(d => d.Name.ToLower().Contains(name.ToLower()))
                .OrderBy(d => d.Name)
                .ToListAsync();
        }
    }
}
