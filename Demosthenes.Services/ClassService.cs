using Demosthenes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Demosthenes.Services
{
    public class ClassService : Service<Class>
    {
        public ClassService(ApplicationDbContext db) : base(db) { }

        public async Task<ICollection<Class>> SearchAsync(string query)
        {
            if (String.IsNullOrEmpty(query))
            {
                return await AllAsync();
            }

            return await db.Classes
                .Where(c => c.Course.Title.Contains(query) || c.Professor.Name.Contains(query))
                .ToListAsync();
        }

        public async Task<ICollection<Class>> EnrollableClasses(string query = null)
        {
            IQueryable<Class> enrollable = db.Classes
                .Where(c => c.Enrollable == true);

            if (!String.IsNullOrEmpty(query))
            {
                enrollable = enrollable.Where(c => c.Course.Title.Contains(query) || c.Professor.Name.Contains(query));
            }

            return await enrollable
                .OrderBy(c => c.CourseId)
                .ThenByDescending(c => c.Id)
                .Include(c => c.Course)
                .Include(c => c.Professor)
                .Include(c => c.Enrollment)
                .Include(c => c.Schedules)
                .ToListAsync();
        }

        public async Task<ICollection<Class>> EnrolledBy(Student student, int? year, Term? term)
        {
            // if year not informed, get current year
            year = year ?? DateTime.Now.Year;
            // if term not informed, get current term
            term = term ?? (Term)(DateTime.Now.Month / 4);

            return await db.Classes
                .Where(c => c.Year == year && c.Term == term && c.Enrollment.Any(e => e.Student.Id == student.Id))
                .Include(c => c.Course)
                .Include(c => c.Schedules)
                .ToListAsync();
        }
    }
}
