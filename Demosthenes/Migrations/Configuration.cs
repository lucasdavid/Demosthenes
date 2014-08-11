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
            //var departments = PopulateDepartments(context);
            //var roles       = PopulateRoles(context);
            //var professors  = PopulateProfessors(context, departments);
            //var courses     = PopulateCourses(context, departments);
            //var students    = PopulateStudents(context);
            //var schedules = PopulateSchedules(context);
            //var classes     = PopulateClasses(context, courses, professors, students);
            //var posts       = PopulatePosts(context, professors);
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
            var random = new Random();

            var courses = new Course[]
            {
                new Course { Title = "Artificial Intelligence", Details = "", Department = departments[0] },
                new Course { Title = "Introduction to Psychology 101", Details = "", Department = departments[1] },
                new Course { Title = "Insectology", Details = "", Department = departments[2] },
                new Course { Title = "International Diplomacy", Details = "", Department = departments[3] },
                new Course { Title = "Calculus I", Details = "", Department = departments[4] },
                new Course { Title = "Computer Architecture", Details = "", Department = departments[5] }
            };

            context.Courses.AddOrUpdate(courses);
            return courses;
        }

        private Student[] PopulateStudents(ApplicationDbContext context)
        {
            var userManager = new UserManager<Student>(new UserStore<Student>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var random = new Random();

            var students = new Student[]
            {
                new Student
                {
                    Name = "Lucas David",
                    Email = "lucas.david@comp.ufscar.edu",
                    UserName = "lucas.david@comp.ufscar.edu",
                    DateBorn = new DateTime(1993, 9, 16)
                },
                new Student
                {
                    Name = "Hellen Page",
                    Email = "hellen@ufscar.br",
                    UserName = "hellen@ufscar.br",
                    DateBorn = new DateTime(1990, 4, 1)
                },
                new Student
                {
                    Name = "John Snow",
                    Email = "john@ufscar.br", UserName = "john@ufscar.br",
                    DateBorn = new DateTime(1995, 12, 30)
                }
            };

            for (int i = 0; i < students.Length; i++)
            {
                if (userManager.FindByEmail(students[i].Email) == null)
                {
                    userManager.Create(students[i], "password");
                    userManager.AddToRole(students[i].Id, "student");
                    userManager.AddToRole(students[i].Id, "admin");
                }
            }

            return students;
        }

        private Class[] PopulateClasses(ApplicationDbContext context, Course[] courses, Professor[] professors, Student[] students)
        {
            // TODO: work on this, populating schedules as well.
            var random = new Random();
            var classes = new Class[]
            {
                new Class
                {
                    Term = (Term) (DateTime.Now.Month / 4),
                    Year = DateTime.Now.Year,
                    Size = (short) random.Next(40, 120),
                    Professor = professors[random.Next(professors.Length)],
                    Course = courses[random.Next(courses.Length)],
                    Enrollable = true
                }
            };

            return classes;
        }

        private Schedule[] PopulateSchedules(ApplicationDbContext context)
        {
            var schedules = new Schedule[]
            {
                new Schedule { Day = DayOfWeek.Sunday, Starting = new TimeSpan(8, 0, 0), Ending = new TimeSpan(10, 0, 0) },
                new Schedule { Day = DayOfWeek.Sunday, Starting = new TimeSpan(10, 0, 0), Ending = new TimeSpan(12, 0, 0) },
                new Schedule { Day = DayOfWeek.Sunday, Starting = new TimeSpan(14, 0, 0), Ending = new TimeSpan(16, 0, 0) },
                new Schedule { Day = DayOfWeek.Sunday, Starting = new TimeSpan(16, 0, 0), Ending = new TimeSpan(18, 0, 0) },
                new Schedule { Day = DayOfWeek.Sunday, Starting = new TimeSpan(19, 0, 0), Ending = new TimeSpan(21, 0, 0) },
                new Schedule { Day = DayOfWeek.Sunday, Starting = new TimeSpan(21, 0, 0), Ending = new TimeSpan(23, 0, 0) },

                new Schedule { Day = DayOfWeek.Monday, Starting = new TimeSpan(8, 0, 0), Ending = new TimeSpan(10, 0, 0) },
                new Schedule { Day = DayOfWeek.Monday, Starting = new TimeSpan(10, 0, 0), Ending = new TimeSpan(12, 0, 0) },
                new Schedule { Day = DayOfWeek.Monday, Starting = new TimeSpan(14, 0, 0), Ending = new TimeSpan(16, 0, 0) },
                new Schedule { Day = DayOfWeek.Monday, Starting = new TimeSpan(16, 0, 0), Ending = new TimeSpan(18, 0, 0) },
                new Schedule { Day = DayOfWeek.Monday, Starting = new TimeSpan(19, 0, 0), Ending = new TimeSpan(21, 0, 0) },
                new Schedule { Day = DayOfWeek.Monday, Starting = new TimeSpan(21, 0, 0), Ending = new TimeSpan(23, 0, 0) },
                
                new Schedule { Day = DayOfWeek.Tuesday, Starting = new TimeSpan(8, 0, 0), Ending = new TimeSpan(10, 0, 0) },
                new Schedule { Day = DayOfWeek.Tuesday, Starting = new TimeSpan(10, 0, 0), Ending = new TimeSpan(12, 0, 0) },
                new Schedule { Day = DayOfWeek.Tuesday, Starting = new TimeSpan(14, 0, 0), Ending = new TimeSpan(16, 0, 0) },
                new Schedule { Day = DayOfWeek.Tuesday, Starting = new TimeSpan(16, 0, 0), Ending = new TimeSpan(18, 0, 0) },
                new Schedule { Day = DayOfWeek.Tuesday, Starting = new TimeSpan(19, 0, 0), Ending = new TimeSpan(21, 0, 0) },
                new Schedule { Day = DayOfWeek.Tuesday, Starting = new TimeSpan(21, 0, 0), Ending = new TimeSpan(23, 0, 0) },

                new Schedule { Day = DayOfWeek.Wednesday, Starting = new TimeSpan(8, 0, 0), Ending = new TimeSpan(10, 0, 0) },
                new Schedule { Day = DayOfWeek.Wednesday, Starting = new TimeSpan(10, 0, 0), Ending = new TimeSpan(12, 0, 0) },
                new Schedule { Day = DayOfWeek.Wednesday, Starting = new TimeSpan(14, 0, 0), Ending = new TimeSpan(16, 0, 0) },
                new Schedule { Day = DayOfWeek.Wednesday, Starting = new TimeSpan(16, 0, 0), Ending = new TimeSpan(18, 0, 0) },
                new Schedule { Day = DayOfWeek.Wednesday, Starting = new TimeSpan(19, 0, 0), Ending = new TimeSpan(21, 0, 0) },
                new Schedule { Day = DayOfWeek.Wednesday, Starting = new TimeSpan(21, 0, 0), Ending = new TimeSpan(23, 0, 0) },

                new Schedule { Day = DayOfWeek.Thursday, Starting = new TimeSpan(8, 0, 0), Ending = new TimeSpan(10, 0, 0) },
                new Schedule { Day = DayOfWeek.Thursday, Starting = new TimeSpan(10, 0, 0), Ending = new TimeSpan(12, 0, 0) },
                new Schedule { Day = DayOfWeek.Thursday, Starting = new TimeSpan(14, 0, 0), Ending = new TimeSpan(16, 0, 0) },
                new Schedule { Day = DayOfWeek.Thursday, Starting = new TimeSpan(16, 0, 0), Ending = new TimeSpan(18, 0, 0) },
                new Schedule { Day = DayOfWeek.Thursday, Starting = new TimeSpan(19, 0, 0), Ending = new TimeSpan(21, 0, 0) },
                new Schedule { Day = DayOfWeek.Thursday, Starting = new TimeSpan(21, 0, 0), Ending = new TimeSpan(23, 0, 0) },

                new Schedule { Day = DayOfWeek.Friday, Starting = new TimeSpan(8, 0, 0), Ending = new TimeSpan(10, 0, 0) },
                new Schedule { Day = DayOfWeek.Friday, Starting = new TimeSpan(10, 0, 0), Ending = new TimeSpan(12, 0, 0) },
                new Schedule { Day = DayOfWeek.Friday, Starting = new TimeSpan(14, 0, 0), Ending = new TimeSpan(16, 0, 0) },
                new Schedule { Day = DayOfWeek.Friday, Starting = new TimeSpan(16, 0, 0), Ending = new TimeSpan(18, 0, 0) },
                new Schedule { Day = DayOfWeek.Friday, Starting = new TimeSpan(19, 0, 0), Ending = new TimeSpan(21, 0, 0) },
                new Schedule { Day = DayOfWeek.Friday, Starting = new TimeSpan(21, 0, 0), Ending = new TimeSpan(23, 0, 0) },

                new Schedule { Day = DayOfWeek.Saturday, Starting = new TimeSpan(8, 0, 0), Ending = new TimeSpan(10, 0, 0) },
                new Schedule { Day = DayOfWeek.Saturday, Starting = new TimeSpan(10, 0, 0), Ending = new TimeSpan(12, 0, 0) },
                new Schedule { Day = DayOfWeek.Saturday, Starting = new TimeSpan(14, 0, 0), Ending = new TimeSpan(16, 0, 0) },
                new Schedule { Day = DayOfWeek.Saturday, Starting = new TimeSpan(16, 0, 0), Ending = new TimeSpan(18, 0, 0) },
                new Schedule { Day = DayOfWeek.Saturday, Starting = new TimeSpan(19, 0, 0), Ending = new TimeSpan(21, 0, 0) },
                new Schedule { Day = DayOfWeek.Saturday, Starting = new TimeSpan(21, 0, 0), Ending = new TimeSpan(23, 0, 0) }
            };

            foreach (Schedule schedule in schedules)
            {
                context.Schedules.AddOrUpdate(schedule);
            }

            return schedules;
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
