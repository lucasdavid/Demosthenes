using Demosthenes.Core;
using Demosthenes.Data;
using Demosthenes.Services.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Demosthenes.Services
{
    public class CourseService : Service<Course>
    {
        public CourseService(DemosthenesContext db) : base(db) { }

        /// <summary>
        /// Retrieves all Courses of a given Department.
        /// </summary>
        /// <param name="departmentId">Id of the Department of which the courses should be recovered.</param>
        /// <returns>A collection of Courses</returns>
        public async Task<ICollection<Course>> OfDepartment(int departmentId)
        {
            return await Db.Courses
                .Where(c => c.DepartmentId == departmentId)
                .ToListAsync();
        }
    }
}
