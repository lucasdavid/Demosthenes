using System;
using Demosthenes.Core.Models;

namespace Demosthenes.Core.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private GenericRepository<Department> departments;
        private GenericRepository<Professor> professors;
        private GenericRepository<Student> students;
        private GenericRepository<ApplicationUser> users;
        private GenericRepository<Course> courses;

        public GenericRepository<Department> Departments
        {
            get
            {
                if (this.departments == null)
                {
                    this.departments = new GenericRepository<Department>(context);
                }
                return departments;
            }
        }

        public GenericRepository<Professor> Professors
        {
            get
            {

                if (this.professors == null)
                {
                    this.professors = new GenericRepository<Professor>(context);
                }
                return professors;
            }
        }

        public GenericRepository<Student> Students
        {
            get
            {

                if (this.students == null)
                {
                    this.students = new GenericRepository<Student>(context);
                }
                return students;
            }
        }

        public GenericRepository<ApplicationUser> Users
        {
            get
            {

                if (this.users == null)
                {
                    this.users = new GenericRepository<ApplicationUser>(context);
                }
                return users;
            }
        }

        public GenericRepository<Course> Courses
        {
            get
            {
                if (this.courses == null)
                {
                    this.courses = new GenericRepository<Course>(context);
                }
                return courses;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
