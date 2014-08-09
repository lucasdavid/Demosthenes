namespace Demosthenes.Migrations
{
    using Demosthenes.Core.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;


    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            var roles       = PopulateRoles(context);

            var departments = PopulateDepartments(context);
            var professors  = PopulateProfessors(context, departments);
            var courses     = PopulateCourses(context, departments);
            var students    = PopulateStudents(context);
            var classes     = PopulateClasses(context, courses, professors, students);
            var posts       = PopulatePosts(context, professors);
        }

        private IdentityRole[] PopulateRoles(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            IdentityRole[] roles = new IdentityRole[]
            {
                new IdentityRole("admin"),
                new IdentityRole("student"),
                new IdentityRole("professor")
            };

            foreach (IdentityRole role in roles)
            {
                if (!roleManager.RoleExists(role.Name))
                {
                    roleManager.Create(role);
                }
            }

            return roles;
        }

        private Class[] PopulateClasses(ApplicationDbContext context, Course[] courses, Professor[] professors, Student[] students)
        {
            var random  = new Random();
            var classes = new Class[]
            {
                new Class
                {
                    Term = Term.Fall, Year = DateTime.Now.Year, Size = 60,
                    Professor = professors[random.Next(professors.Length)],
                    Course = courses[random.Next(courses.Length)],
                    Enrollable = true
                }
            };

            return classes;
        }

        private Department[] PopulateDepartments(ApplicationDbContext context)
        {
            var departments = new Department[]
            {
                new Department { Name = "Computer Science" },
                new Department { Name = "Psychology" },
                new Department { Name = "Biology" },
                new Department { Name = "Political Science" },
                new Department { Name = "Mathematics" },
                new Department { Name = "Computer Engineer" }
            };

            context.Departments.AddOrUpdate(departments);
            return departments;
        }

        private Professor[] PopulateProfessors(ApplicationDbContext context, Department[] departments)
        {
            var userManager = new UserManager<Professor>(new UserStore<Professor>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var professors = new Professor[]
            {
                new Professor { Email = "johnpaul@ufscar.edu", Name = "John Paul", SSN = "102-25-3921", Department = departments[0] },
                new Professor { Email = "cris.h@ufscar.edu", Name = "Cristina Hulfman", SSN = "102-21-1877", Department = departments[1] },
                new Professor { Email = "christian@ufscar.edu", Name = "Christian Bale", SSN = "622-21-0381", Department = departments[1] },
                new Professor { Email = "david.wilson@ufscar.edu", Name = "David Wilson", SSN = "902-81-8375", Department = departments[2] },
                new Professor { Email = "maria@ufscar.edu",Name = "Maria Galles", SSN = "392-71-6461", Department = departments[3] },
                new Professor { Email = "jason@ufscar.edu", Name = "Jason Bourne", SSN = "017-31-3315", Department = departments[4] },
                new Professor { Email = "hellen@ufscar.edu",Name = "Hellen Page", SSN = "847-27-0981", Department = departments[3] }
            };

            foreach (Professor professor in professors)
            {
                professor.UserName = professor.Email;
                if (userManager.FindByEmail(professor.Email) == null)
                {
                    userManager.Create(professor, "password");
                    userManager.AddToRole(professor.Id, "professor");
                }
            }

            return professors;
        }

        private Course[] PopulateCourses(ApplicationDbContext context, Department[] departments)
        {
            var courses = new Course[10];

            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                courses[i] = new Course
                {
                    Title = "Course " + i,
                    Details = "Details of course " + i,
                    Department = departments[random.Next(departments.Length)]
                };
            }

            context.Courses.AddOrUpdate(courses);
            return courses;
        }

        private Student[] PopulateStudents(ApplicationDbContext context)
        {
            var userManager = new UserManager<Student>(new UserStore<Student>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var students = new Student[10];
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                students[i] = new Student
                {
                    Name = "Student " + i,
                    Email = "student" + i + "@ufscar.edu",
                    UserName = "student" + i + "@ufscar.edu",
                    DateBorn = DateTime.Now.AddYears(-random.Next(14, 50))
                };

                if (userManager.FindByEmail(students[i].Email) == null)
                {
                    userManager.Create(students[i], "password");
                    userManager.AddToRole(students[i].Id, "student");
                }
            }

            return students;
        }

        private Post[] PopulatePosts(ApplicationDbContext context, Professor[] professors)
        {
            var random = new Random();
            var posts = new Post[10];

            for (int i = 0; i < 10; i++)
            {
                posts[i] = new Post
                {
                    Title = "Our very " + i + "n post",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Author = professors[random.Next(professors.Length)]
                };
            }

            context.Posts.AddOrUpdate(posts);
            return posts;
        }
    }
}
