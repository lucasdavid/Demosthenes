using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Demosthenes.Core.Models;
using System.Collections.Generic;
using System.Linq;
using Demosthenes.Services;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace Demosthenes.Tests.Services
{
    [TestClass]
    public class CourseServiceTest
    {
        private Mock<ApplicationDbContext> context;

        [TestInitialize]
        public void Setup()
        {
            context = new Mock<ApplicationDbContext>();

            var courses = new List<Course>
            {
                new Course { Id = 1, Title = "Artificial Intelligence", Details = "", Department = new Department { Name = "Computer Science" } },
                new Course { Id = 2, Title = "Psychology 101", Details = "", Department = new Department { Name = "Psychology" } },
            }.AsQueryable();

            var coursesSet = new Mock<DbSet<Course>>();
            coursesSet.As<IDbAsyncEnumerable<Course>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Course>(courses.GetEnumerator()));
            coursesSet.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Course>(courses.Provider));
            coursesSet.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(courses.Expression);
            coursesSet.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(courses.ElementType);
            coursesSet.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(courses.GetEnumerator());
            context.Setup(c => c.Set<Course>()).Returns(coursesSet.Object);
            context.Setup(c => c.Courses).Returns(coursesSet.Object);

            context.Setup(c => c.Set<Course>()).Returns(coursesSet.Object);
            context.Setup(c => c.Courses).Returns(coursesSet.Object);
        }

        [TestMethod]
        public async Task CourseSearchWithoutQuery()
        {
            var service = new CourseService(context.Object);
            var result = await service.SearchAsync(null);

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task CourseSearchWithQuery()
        {
            var service = new CourseService(context.Object);
            var result = await service.SearchAsync("101");

            Assert.AreEqual(2, result.First().Id);
        }
    }
}
