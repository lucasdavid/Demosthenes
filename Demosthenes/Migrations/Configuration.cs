namespace Demosthenes.Migrations
{
    using Demosthenes.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Demosthenes.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Demosthenes.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            PopulateDepartments(context);
            PopulateProfessors(context);
            PopulateCourses(context);
        }

        private void PopulateDepartments(ApplicationDbContext context)
        {
            var departments = new Department[10];

            for (int i = 0; i < 10; i++)
            {
                departments[i] = new Department
                {
                    Name = "The beautiful department " + i
                };
            }

            context.Departments.AddOrUpdate(departments);
        }

        private void PopulateProfessors(ApplicationDbContext context)
        {
            var professors = new Professor[10];

            var random = new Random();
            var departments = context.Departments.ToList();

            for (int i = 0; i < 10; i++)
            {
                professors[i] = new Professor
                {
                    Name = "A nice professor called " + i,
                    SSN  = random.Next(100000).ToString(),
                    Department = departments[random.Next(departments.Count)]
                };
            }

            context.Professors.AddOrUpdate(professors);
        }

        private void PopulateCourses(ApplicationDbContext context)
        {
            var courses = new Course[10];

            var random = new Random();
            var departments = context.Departments.ToList();

            for (int i = 0; i < 10; i++)
            {
                courses[i] = new Course
                {
                    Title = "Course " + i,
                    Details = "Details of course " + i,
                    Department = departments[random.Next(departments.Count)]
                };
            }

            context.Courses.AddOrUpdate(courses);
        }
    }
}
