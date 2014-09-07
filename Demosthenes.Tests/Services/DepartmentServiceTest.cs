using Demosthenes.Core.Models;
using Demosthenes.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Demosthenes.Tests.Services
{
    [TestClass]
    public class DepartmentServiceTest
    {
        private Mock<ApplicationDbContext> context;

        [TestInitialize]
        public void Setup()
        {
            var data = new List<Department>
            {
                new Department { Id = 1, Name = "Computer Science" },
                new Department { Id = 2, Name = "Political Science" },
                new Department { Id = 3, Name = "Physics" },
                new Department { Id = 4, Name = "Mathematics" },
            }.AsQueryable();

            var set = new Mock<DbSet<Department>>() { CallBase = true };
            set.As<IDbAsyncEnumerable<Department>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Department>(data.GetEnumerator()));
            set.As<IQueryable<Department>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Department>(data.Provider));
            set.As<IQueryable<Department>>().Setup(m => m.Expression).Returns(data.Expression);
            set.As<IQueryable<Department>>().Setup(m => m.ElementType).Returns(data.ElementType);
            set.As<IQueryable<Department>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            context = new Mock<ApplicationDbContext>();
            context.Setup(c => c.Set<Department>()).Returns(set.Object);
            context.Setup(c => c.Departments).Returns(set.Object);
        }

        [TestMethod]
        public async Task DepartmentsSearchWithoutQuery()
        {
            var service = new DepartmentService(context.Object);
            var result = await service.SearchAsync(null);
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public async Task DepartmentsSearchWithQuery()
        {
            var name = "Science";

            var service = new DepartmentService(context.Object);
            var result = await service.SearchAsync(name);
            Assert.AreEqual(2, result.Count);

            name = "Mathematics";
            result = await service.SearchAsync(name);
            Assert.AreEqual(1, result.Count);

            name = "P";
            result = await service.SearchAsync(name);
            Assert.AreEqual(3, result.Count);
        }
    }
}
