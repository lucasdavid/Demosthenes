using Demosthenes.Controllers;
using Demosthenes.Core.Models;
using Demosthenes.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Demosthenes.Tests.Controllers
{
    [TestClass]
    public class DepartmentsControllerTest
    {
        private DepartmentService service;

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

            var set = new Mock<DbSet<Department>>();
            set.As<IDbAsyncEnumerable<Department>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Department>(data.GetEnumerator()));
            set.As<IQueryable<Department>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Department>(data.Provider));
            set.As<IQueryable<Department>>().Setup(m => m.Expression).Returns(data.Expression);
            set.As<IQueryable<Department>>().Setup(m => m.ElementType).Returns(data.ElementType);
            set.As<IQueryable<Department>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var context = new Mock<ApplicationDbContext>();
            context.Setup(c => c.Set<Department>()).Returns(set.Object);
            context.Setup(c => c.Departments).Returns(set.Object);

            service = new DepartmentService(context.Object);
        }

        [TestMethod]
        public async Task Index()
        {
            var request = new Mock<HttpRequestBase>();
            var context = new Mock<HttpContextBase>();

            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new DepartmentsController(service);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = await controller.Index();
            var viewmodel = controller.ViewData.Model as IPagedList<Department>;

            Assert.IsNotNull(result);
            Assert.AreEqual(4, viewmodel.Count(), "Unexpected number of departments");
        }

        [TestMethod]
        public async Task IndexWithQParam()
        {
            var request = new Mock<HttpRequestBase>();
            var context = new Mock<HttpContextBase>();

            context.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new DepartmentsController(service);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = await controller.Index("computer");
            var viewmodel = controller.ViewData.Model as IPagedList<Department>;

            Assert.IsNotNull(result);
            Assert.IsTrue(viewmodel.Count() == 1, "viewmodel.Count(): " + viewmodel.Count());
        }

        [TestMethod]
        public void Create()
        {
            var controller = new DepartmentsController(service);
            var result     = controller.Create() as ActionResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(String.IsNullOrEmpty(controller.ViewBag.Title));
            Assert.IsTrue(String.IsNullOrEmpty(controller.ViewBag.Lead));
        }
    }
}
