using Demosthenes.Core.Models;

namespace Demosthenes.Services
{
    public class StudentService : Service<Student>
    {
        public StudentService(ApplicationDbContext db) : base(db) { }
    }
}
