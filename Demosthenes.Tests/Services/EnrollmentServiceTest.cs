using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Demosthenes.Core.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Demosthenes.Services;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using Demosthenes.Core.Exceptions.Enrollment;

namespace Demosthenes.Tests.Services
{
    [TestClass]
    public class EnrollmentServiceTest
    {
        private Mock<ApplicationDbContext> context;
        private IQueryable<Enrollment> enrollments;

        [TestInitialize]
        public void Setup()
        {
            context = new Mock<ApplicationDbContext>();

            enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                    Id = 1, StudentId = "student", ClassId = 1,
                    Class = new Class { Enrollable = false }
                },
                new Enrollment
                {
                    Id = 2, StudentId = "student", ClassId = 2,
                    Class = new Class { Enrollable = true }
                },
                new Enrollment
                {
                    Id = 3, StudentId = "otherstudent", ClassId = 4,
                    Class = new Class { Enrollable = true, Size = 1 }
                },
            }.AsQueryable();

            var enrollmentsSet = new Mock<DbSet<Enrollment>>();
            enrollmentsSet.As<IDbAsyncEnumerable<Enrollment>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Enrollment>(enrollments.GetEnumerator()));
            enrollmentsSet.As<IQueryable<Enrollment>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Enrollment>(enrollments.Provider));
            enrollmentsSet.As<IQueryable<Enrollment>>().Setup(m => m.Expression).Returns(enrollments.Expression);
            enrollmentsSet.As<IQueryable<Enrollment>>().Setup(m => m.ElementType).Returns(enrollments.ElementType);
            enrollmentsSet.As<IQueryable<Enrollment>>().Setup(m => m.GetEnumerator()).Returns(enrollments.GetEnumerator());
            context.Setup(c => c.Set<Enrollment>()).Returns(enrollmentsSet.Object);
            context.Setup(c => c.Enrollments).Returns(enrollmentsSet.Object);

            context.Setup(c => c.Set<Enrollment>()).Returns(enrollmentsSet.Object);
            context.Setup(c => c.Enrollments).Returns(enrollmentsSet.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(NonEnrollableClassException))]
        public async Task UnenrollStudentFromUnenrollableClass()
        {
            var service = new EnrollmentService(context.Object);
            var student = new Student { Id = "student" };
            
            var result  = await service.Unenroll(student, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownStudentException))]
        public async Task UnenrollNotEnrolledStudent()
        {
            var service = new EnrollmentService(context.Object);
            var student = new Student { Id = "otherstudent" };

            var result = await service.Unenroll(student, 1);
        }

        [TestMethod]
        public async Task ValidUnenrollmentStudent()
        {
            var service    = new EnrollmentService(context.Object);
            var student    = new Student { Id = "student" };

            var enrollment = await service.Unenroll(student, 2);

            Assert.IsTrue(enrollment.Id == 2);
        }
    }
}
