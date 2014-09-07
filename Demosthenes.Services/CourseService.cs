using Demosthenes.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Demosthenes.Services
{
    public class CourseService : Service<Course>
    {
        public CourseService(ApplicationDbContext db) : base(db) { }

        public async Task<ICollection<Course>> SearchAsync(string query)
        {
            if (String.IsNullOrEmpty(query))
            {
                return await AllAsync();
            }

            return await db.Courses
                .Where(c => c.Title.Contains(query) || c.Details.Contains(query) || c.Department.Name.Contains(query))
                .Include(c => c.Department)
                .ToListAsync();
        }
    }
}
