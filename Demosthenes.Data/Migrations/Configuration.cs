namespace Demosthenes.Data.Migrations
{
    using Demosthenes.Core;
    using Demosthenes.Core.Enums;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal class Configuration : DbMigrationsConfiguration<Demosthenes.Data.DemosthenesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Demosthenes.Data.DemosthenesContext context)
        {
            SeedRoles(context);
            SeedSchedules(context);
            SeedDepartments(context);
            SeedCourses(context);
            //SeedStudents(context);
            //SeedProfessors(context);
            //SeedAdmins(context);
            SeedClasses(context);
            //SeedClassSchedules(context);
        }

        protected void SeedRoles(DemosthenesContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            manager.Create(new IdentityRole { Name = "professor" });
            manager.Create(new IdentityRole { Name = "student" });
            manager.Create(new IdentityRole { Name = "admin" });
        }
        protected void SeedSchedules(DemosthenesContext context)
        {
            if (!context.Schedules.Any())
            {
                var schedules = new List<Schedule>
                {
                    new Schedule { TimeStarted = new TimeSpan(8, 0, 0), TimeFinished = new TimeSpan(10, 0, 0), DayOfWeek = DayOfWeek.Sunday },
                    new Schedule { TimeStarted = new TimeSpan(10, 0, 0), TimeFinished = new TimeSpan(12, 0, 0), DayOfWeek = DayOfWeek.Sunday },
                    new Schedule { TimeStarted = new TimeSpan(14, 0, 0), TimeFinished = new TimeSpan(16, 0, 0), DayOfWeek = DayOfWeek.Sunday },
                    new Schedule { TimeStarted = new TimeSpan(16, 0, 0), TimeFinished = new TimeSpan(18, 0, 0), DayOfWeek = DayOfWeek.Sunday },
                    new Schedule { TimeStarted = new TimeSpan(19, 0, 0), TimeFinished = new TimeSpan(21, 0, 0), DayOfWeek = DayOfWeek.Sunday },
                    new Schedule { TimeStarted = new TimeSpan(21, 0, 0), TimeFinished = new TimeSpan(23, 0, 0), DayOfWeek = DayOfWeek.Sunday },

                    new Schedule { TimeStarted = new TimeSpan(8, 0, 0), TimeFinished = new TimeSpan(10, 0, 0), DayOfWeek = DayOfWeek.Monday },
                    new Schedule { TimeStarted = new TimeSpan(10, 0, 0), TimeFinished = new TimeSpan(12, 0, 0), DayOfWeek = DayOfWeek.Monday },
                    new Schedule { TimeStarted = new TimeSpan(14, 0, 0), TimeFinished = new TimeSpan(16, 0, 0), DayOfWeek = DayOfWeek.Monday },
                    new Schedule { TimeStarted = new TimeSpan(16, 0, 0), TimeFinished = new TimeSpan(18, 0, 0), DayOfWeek = DayOfWeek.Monday },
                    new Schedule { TimeStarted = new TimeSpan(19, 0, 0), TimeFinished = new TimeSpan(21, 0, 0), DayOfWeek = DayOfWeek.Monday },
                    new Schedule { TimeStarted = new TimeSpan(21, 0, 0), TimeFinished = new TimeSpan(23, 0, 0), DayOfWeek = DayOfWeek.Monday },

                    new Schedule { TimeStarted = new TimeSpan(8, 0, 0), TimeFinished = new TimeSpan(10, 0, 0), DayOfWeek = DayOfWeek.Tuesday },
                    new Schedule { TimeStarted = new TimeSpan(10, 0, 0), TimeFinished = new TimeSpan(12, 0, 0), DayOfWeek = DayOfWeek.Tuesday },
                    new Schedule { TimeStarted = new TimeSpan(14, 0, 0), TimeFinished = new TimeSpan(16, 0, 0), DayOfWeek = DayOfWeek.Tuesday },
                    new Schedule { TimeStarted = new TimeSpan(16, 0, 0), TimeFinished = new TimeSpan(18, 0, 0), DayOfWeek = DayOfWeek.Tuesday },
                    new Schedule { TimeStarted = new TimeSpan(19, 0, 0), TimeFinished = new TimeSpan(21, 0, 0), DayOfWeek = DayOfWeek.Tuesday },
                    new Schedule { TimeStarted = new TimeSpan(21, 0, 0), TimeFinished = new TimeSpan(23, 0, 0), DayOfWeek = DayOfWeek.Tuesday },

                    new Schedule { TimeStarted = new TimeSpan(8, 0, 0), TimeFinished = new TimeSpan(10, 0, 0), DayOfWeek = DayOfWeek.Wednesday },
                    new Schedule { TimeStarted = new TimeSpan(10, 0, 0), TimeFinished = new TimeSpan(12, 0, 0), DayOfWeek = DayOfWeek.Wednesday },
                    new Schedule { TimeStarted = new TimeSpan(14, 0, 0), TimeFinished = new TimeSpan(16, 0, 0), DayOfWeek = DayOfWeek.Wednesday },
                    new Schedule { TimeStarted = new TimeSpan(16, 0, 0), TimeFinished = new TimeSpan(18, 0, 0), DayOfWeek = DayOfWeek.Wednesday },
                    new Schedule { TimeStarted = new TimeSpan(19, 0, 0), TimeFinished = new TimeSpan(21, 0, 0), DayOfWeek = DayOfWeek.Wednesday },
                    new Schedule { TimeStarted = new TimeSpan(21, 0, 0), TimeFinished = new TimeSpan(23, 0, 0), DayOfWeek = DayOfWeek.Wednesday },

                    new Schedule { TimeStarted = new TimeSpan(8, 0, 0), TimeFinished = new TimeSpan(10, 0, 0), DayOfWeek = DayOfWeek.Thursday },
                    new Schedule { TimeStarted = new TimeSpan(10, 0, 0), TimeFinished = new TimeSpan(12, 0, 0), DayOfWeek = DayOfWeek.Thursday },
                    new Schedule { TimeStarted = new TimeSpan(14, 0, 0), TimeFinished = new TimeSpan(16, 0, 0), DayOfWeek = DayOfWeek.Thursday },
                    new Schedule { TimeStarted = new TimeSpan(16, 0, 0), TimeFinished = new TimeSpan(18, 0, 0), DayOfWeek = DayOfWeek.Thursday },
                    new Schedule { TimeStarted = new TimeSpan(19, 0, 0), TimeFinished = new TimeSpan(21, 0, 0), DayOfWeek = DayOfWeek.Thursday },
                    new Schedule { TimeStarted = new TimeSpan(21, 0, 0), TimeFinished = new TimeSpan(23, 0, 0), DayOfWeek = DayOfWeek.Thursday },
    
                    new Schedule { TimeStarted = new TimeSpan(8, 0, 0), TimeFinished = new TimeSpan(10, 0, 0), DayOfWeek = DayOfWeek.Friday },
                    new Schedule { TimeStarted = new TimeSpan(10, 0, 0), TimeFinished = new TimeSpan(12, 0, 0), DayOfWeek = DayOfWeek.Friday },
                    new Schedule { TimeStarted = new TimeSpan(14, 0, 0), TimeFinished = new TimeSpan(16, 0, 0), DayOfWeek = DayOfWeek.Friday },
                    new Schedule { TimeStarted = new TimeSpan(16, 0, 0), TimeFinished = new TimeSpan(18, 0, 0), DayOfWeek = DayOfWeek.Friday },
                    new Schedule { TimeStarted = new TimeSpan(19, 0, 0), TimeFinished = new TimeSpan(21, 0, 0), DayOfWeek = DayOfWeek.Friday },
                    new Schedule { TimeStarted = new TimeSpan(21, 0, 0), TimeFinished = new TimeSpan(23, 0, 0), DayOfWeek = DayOfWeek.Friday },

                    new Schedule { TimeStarted = new TimeSpan(8, 0, 0), TimeFinished = new TimeSpan(10, 0, 0), DayOfWeek = DayOfWeek.Saturday },
                    new Schedule { TimeStarted = new TimeSpan(10, 0, 0), TimeFinished = new TimeSpan(12, 0, 0), DayOfWeek = DayOfWeek.Saturday },
                    new Schedule { TimeStarted = new TimeSpan(14, 0, 0), TimeFinished = new TimeSpan(16, 0, 0), DayOfWeek = DayOfWeek.Saturday },
                    new Schedule { TimeStarted = new TimeSpan(16, 0, 0), TimeFinished = new TimeSpan(18, 0, 0), DayOfWeek = DayOfWeek.Saturday },
                    new Schedule { TimeStarted = new TimeSpan(19, 0, 0), TimeFinished = new TimeSpan(21, 0, 0), DayOfWeek = DayOfWeek.Saturday },
                    new Schedule { TimeStarted = new TimeSpan(21, 0, 0), TimeFinished = new TimeSpan(23, 0, 0), DayOfWeek = DayOfWeek.Saturday }
                };

                context.Schedules.AddRange(schedules);
            }
        }
        protected void SeedDepartments(DemosthenesContext context)
        {
            if (!context.Departments.Any())
            {
                var departments = new List<Department>
                {
                    new Department { Name = "Accounting and Legal Studies" },
                    new Department { Name = "Anthropology" },
                    new Department { Name = "Biology" },
                    new Department { Name = "Chemistry and Biochemistry" },
                    new Department { Name = "Communication" },
                    new Department { Name = "Computer Science" },
                    new Department { Name = "Economics and Finance" },
                    new Department { Name = "Geology and Environmental Geosciences" },
                    new Department { Name = "Health and Human Performance" },
                    new Department { Name = "History" },
                    new Department { Name = "International and Intercultural Studies" },
                    new Department { Name = "Management and Entrepreneurship" },
                    new Department { Name = "Marketing and Supply Chain Management" },
                    new Department { Name = "Mathematics" },
                    new Department { Name = "Music" },
                    new Department { Name = "Philosophy" },
                    new Department { Name = "Physics and Astronomy" },
                    new Department { Name = "Political Science" },
                    new Department { Name = "Psychology" },
                    new Department { Name = "Sociology" }
                };

                context.Departments.AddRange(departments);
            }
        }
        protected void SeedProfessors(DemosthenesContext context)
        {
            if (!context.Professors.Any())
            {
                var professors = new List<Professor>
                {
                    new Professor { UserName = "hm@d.edu", Name = "Hobert Mehan" },
                    new Professor { UserName = "fb@d.edu", Name = "Felisa Breland" },
                    new Professor { UserName = "ap@d.edu", Name = "Ada Paterno" },
                    new Professor { UserName = "sc@d.edu", Name = "Sharyl Causey" },
                    new Professor { UserName = "wo@d.edu", Name = "Wm Ornelas" },
                    new Professor { UserName = "sb@d.edu", Name = "Samuel Benn" },
                    new Professor { UserName = "dm@d.edu", Name = "Deedee Manthey" },
                    new Professor { UserName = "jt@d.edu", Name = "Jolie Torrey" },
                    new Professor { UserName = "rd@d.edu", Name = "Romona Deshotel" },
                    new Professor { UserName = "ss@d.edu", Name = "Shanika Seldon" },
                    new Professor { UserName = "rt@d.edu", Name = "Renee Troup" },
                    new Professor { UserName = "cm@d.edu", Name = "Chasidy Mcginn" },
                    new Professor { UserName = "aw@d.edu", Name = "Agustin Watterson" },
                    new Professor { UserName = "ks@d.edu", Name = "Kayleigh Sechrest" },
                    new Professor { UserName = "ht@d.edu", Name = "Holley Tolliver" },
                    new Professor { UserName = "rb@d.edu", Name = "Richie Baize" },
                    new Professor { UserName = "zc@d.edu", Name = "Zula Cover" },
                    new Professor { UserName = "ts@d.edu", Name = "Trevor Shirley" },
                    new Professor { UserName = "am@d.edu", Name = "Alysa Mcmackin" },
                    new Professor { UserName = "ta@d.edu", Name = "Theron Albee" },
                    new Professor { UserName = "km@d.edu", Name = "Keturah Mccullough" },
                    new Professor { UserName = "sm@d.edu", Name = "Shelli Murph" },
                    new Professor { UserName = "se@d.edu", Name = "Sherley Ellithorpe" },
                    new Professor { UserName = "ms@d.edu", Name = "Mozell Sansone" },
                    new Professor { UserName = "jd@d.edu", Name = "Jannie Dingler" },
                    new Professor { UserName = "kl@d.edu", Name = "Keva Leno" },
                    new Professor { UserName = "nm@d.edu", Name = "Narcisa Mazzola" },
                    new Professor { UserName = "kol@d.edu", Name = "Kory Leiser" },
                    new Professor { UserName = "gj@d.edu", Name = "Gerald Jandreau" },
                    new Professor { UserName = "kp@d.edu", Name = "Kristan Price" },
                    new Professor { UserName = "lr@d.edu", Name = "Luanna Ryles" },
                    new Professor { UserName = "lk@d.edu", Name = "Le Kelson" },
                    new Professor { UserName = "bh@d.edu", Name = "Bernice Hetzel" },
                    new Professor { UserName = "cam@d.edu", Name = "Camie Mimms" },
                    new Professor { UserName = "jb@d.edu", Name = "Jeanice Brasch" },
                    new Professor { UserName = "ew@d.edu", Name = "Eryn Walkes" },
                    new Professor { UserName = "tv@d.edu", Name = "Twana Vallarta" },
                    new Professor { UserName = "cg@d.edu", Name = "Carmen Glance" }
                };

                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var random = new Random();

                var departments = context.Departments.ToList();

                foreach (var professor in professors)
                {
                    professor.Email = professor.UserName;
                    professor.SSN = "000-000-0000";
                    professor.PhoneNumber = "999-999-9999";
                    professor.DepartmentId = departments[random.Next(departments.Count - 1)].Id;

                    manager.Create(professor, "password");
                    manager.AddToRole(professor.Id, "professor");
                }
            }
        }
        protected void SeedAdmins(DemosthenesContext context)
        {
            var admins = new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "admin@admin.com", Name = "Administrator" }
            };

            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            foreach (var admin in admins)
            {
                admin.Email = admin.UserName;

                manager.Create(admin, "password");
                manager.AddToRole(admin.Id, "admin");
            }
        }
        protected void SeedCourses(DemosthenesContext context)
        {
            if (!context.Courses.Any())
            {
                var departments = context.Departments.ToList();

                var courses = new List<Course>
                {
                    new Course { Title = "Advanced Advocacy: Problems and Techniques (LAW451H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Advanced Civil Procedure (LAW201H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Advanced Constitutional Law: Security and Remedial Issues (LAW541H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Advanced Labour and Employment Law (WITHDRAWN) (LAW343H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Advanced Legal Research, Analysis and Writing (0101) (LAW307H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Advanced Private Law: Categories and Concepts (LAW230H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Alternative Approaches to Legal Scholarship (Graduate Students only) (LAW245H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Alternative Dispute Resolution in the Legal Environment (LAW522H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Art of the Deal (LAW300H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Bankruptcy Law (LAW408H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Business Organizations (LAW212H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Canadian Income Tax Law (LAW284H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Canadian Legal History (LAW354H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Canadian Legal Methods and Writing (LAW395H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Canadian Migration Law (LAW456H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Capstone Course: The Role of the Judge (LAW603H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Children and Families (LAW417H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Class Actions Law (LAW462H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education Aboriginal Legal Services of Toronto (0104) (LAW248Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education Advocates for Injured Workers (0101) (LAW248Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education Barbra Schlifer Clinic (0102) (LAW248Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education Barbra Schlifer Clinic Intensive (LAW728H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education Connect Legal (LAW559H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education David Asper Centre for Constitutional Rights (LAW391H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education Innovation Law Clinic at MaRS (WITHDRAWN) (LAW485Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education International Human Rights Clinic (LAW548H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education International Human Rights Clinic Practicum (LAW538H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education: Downtown Legal Services - Intensive Program (Clinic) (LAW402H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education: Downtown Legal Services - Intensive Program (Paper) (LAW562H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education: Downtown Legal Services - Criminal Law Clinic (0101) (LAW209H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education: Downtown Legal Services - Family Law Clinic (0102) (LAW209H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education: Downtown Legal Services - Refugee and Immigration Law Clinic (0103) (LAW209H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education: Downtown Legal Services - Tenant Housing Clinic (0104) (LAW209H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Clinical Legal Education: Downtown Legal Services - University Affairs Clinic (0105) (LAW209H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Comparative Constitutional Law and Politics (JPJ20471H1F) (LAW409H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Competition Policy (LAW312H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Constitutional Law of the United States (LAW333H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Copyright Law (LAW383H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Corporations, Individuals, and the State (LAW288H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Crime & Punishment: Mandatory Minimums, The Death Penalty & other Current Debates (LAW251H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Digital Content and the Creative Economy (LAW450H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Directed Research - Graduate Students only (0102) (LAW291H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Directed Research Program (LAW291H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Directed Research Program (LAW291Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Environmental Law (LAW239H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Ethics in the Business Law Setting (LAW525H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Evidence Law (0101) (LAW241H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Evidence Law (0102) (LAW241H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Exploring the Intersections of Law and Social Work (LAW345Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Fiduciary Law (LAW240H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Finance, Business and Accounting in the Law (LAW250H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Financial Crimes and Corporate Compliance (LAW325H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Freedom of Expression and Press (LAW346H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Health Law and Bioethics (LAW267H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Homicide (LAW560H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Intensive Course (graduate students only): Introduction to the Canadian Legal System (LAW535H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Intensive Course: Brecht: A Case Study in Law and Literature (WITHDRAWN) (LAW708H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Intensive Course: International Intellectual Property and Development (LAW717H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Intensive Course: Introduction to the Legal System of the People's Republic of China (LAW265H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Intensive Course: Key Concepts in Trade Mark Law (LAW721H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Intensive Course: Proportionality, Constitutional Rights And Their Limitations (LAW338H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "International Criminal Law: Genocide, Crimes against Humanity, & War Crimes (LAW385H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "International Trade Regulation (JPJ2037) (ECO3504HF) (ECO459H1F) (LAW285H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Issues in Aboriginal Law and Policy (LAW281H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Journal: Critical Analysis of Law - An International & Interdisciplinary Law Review (LAW479Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Journal: Indigenous Law (LAW494Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Journal: International Law and International Relations (LAW580Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Journal: Law and Equality (LAW493Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Journal: University of Toronto Faculty of Law Review (LAW380Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Judicial Decision-Making (LAW466H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Labour and Employment Law (LAW263H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Law and Literature (English 6552H) (LAW355H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Law and Multiculturalism (LAW382H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Law of International Business & Finance Transactions (LAW371H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Legal Ethics and Lawyer Regulation Intensive (LAW287H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Legal Process, Professionalism and Ethics (LAW199H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Litigation and Social Change (LAW316H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "LLM THESIS (LAW9999YY)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Mentally Disordered Accused (WITHDRAWN) (LAW336H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Moot - Grand Moot Competitive Program (LAW430H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Moot - Jessup Competitive Program - (0101) (LAW430Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Moot - Kawaskimhon Moot Competitive Program & Advanced Aboriginal Studies Competitive Program (LAW331Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Negotiation (LAW272H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Occupational Health & Safety (3260.03) (OSG249H0F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Perspectives on Law (LAW219H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Public International Law (LAW252H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Real Estate Law (LAW275H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Religion and the Liberal State: The Case of Islam (RLG3501H) (JPJ2029H) (LAW321H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Remedies (LAW276H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Remedies (WITHDRAWN) (LAW276H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Reproductive Health Law in Transnational Perspective (WGS1027H1) (LAW386H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Scientific Evidence: Its use and abuse in the law (WITHDRAWN) (LAW465H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Securities Regulation (LAW293H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Sentencing and Penal Policy (CRI 3355H1F) (LAW323H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Shareholder Activism (LAW468H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Sports Law (LAW256H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Student Scholarship Workshop (LAW505H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Supervised Upper Year Research Paper (SUYRP) (LAW599H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Telecommunications and Internet Law (LAW223H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Theory of the Private Law: Selected Topics and Texts (LAW368H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Trial Advocacy (LAW205H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Trusts (LAW233H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Wills and Estate Planning (LAW340H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Women, Violence, and the Law (LAW529H1F)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Workshop: Contemporary Issues in Health Law, Ethics and Policy (LAW501Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Workshop: Critical Analysis of Law (LAW221Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Workshop: Innovation Law and Policy (LAW365Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Workshop: Law and Economics Seminar (LAW399Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Workshop: Tax Law and Policy (LAW211Y1Y)", Credits = 4, DepartmentId = departments.First().Id },
                    new Course { Title = "Youth Criminal Justice (LAW311H1F)", Credits = 4, DepartmentId = departments.First().Id }
                };

                context.Courses.AddRange(courses);
            }
        }
        protected void SeedStudents(DemosthenesContext context)
        {
            if (!context.Students.Any())
            {
                var students = new List<Student>
                {
                    new Student { UserName = "ec@d.edu", Name = "Erlene Conrad" },
                    new Student { UserName = "adw@d.edu", Name = "Adela Whitwell" },
                    new Student { UserName = "db@d.edu", Name = "Deshawn Bayliss" },
                    new Student { UserName = "hd@d.edu", Name = "Hedy Dumond" },
                    new Student { UserName = "ab@d.edu", Name = "Arthur Biggins" },
                    new Student { UserName = "fa@d.edu", Name = "Felecia Auguste" },
                    new Student { UserName = "ml@d.edu", Name = "Margarette Laney" },
                    new Student { UserName = "oo@d.edu", Name = "Odessa Orso" },
                    new Student { UserName = "rg@d.edu", Name = "Rosita Gruber" },
                    new Student { UserName = "ll@d.edu", Name = "Long Luton" },
                    new Student { UserName = "ck@d.edu", Name = "Carolee Kampf" },
                    new Student { UserName = "rc@d.edu", Name = "Roselia Chauncey" }
                };

                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                foreach (var student in students)
                {
                    student.Email = student.UserName;
                    manager.Create(student, "password");
                    manager.AddToRole(student.Id, "student");
                }
            }
        }
        protected void SeedClasses(DemosthenesContext context)
        {
            if (!context.Classes.Any())
            {
                var random = new Random();

                var courses = context.Courses.ToList();
                var professors = context.Professors.ToList();
                var termsCount = Enum.GetValues(typeof(Term)).Length;
                for (int i = 0; i < 100; i++)
                {
                    context.Classes.Add(new Class
                    {
                        CourseId = courses[random.Next(courses.Count - 1)].Id,
                        Enrollable = random.NextDouble() > .5,
                        Size = (int)(random.NextDouble() * 100),
                        Term = (Term)(random.Next(termsCount - 1)),
                        Year = 2015,
                        ProfessorId = professors[random.Next(professors.Count - 1)].Id
                    });
                }
            }
        }
        protected void SeedClassSchedules(DemosthenesContext context)
        {
            var classes = context.Classes.ToList();
            var schedules = context.Schedules.ToList();
            var random = new Random();

            foreach (var c in classes)
            {
                var numberOfSchedules = random.Next(4);
                var localSchedules    = new List<Schedule>(schedules);

                for (var i = 0; i < numberOfSchedules; i++)
                {
                    var index    = random.Next(localSchedules.Count);
                    var schedule = localSchedules[index];
                    localSchedules.Remove(schedule);

                    c.Schedules.Add(schedule);
                }
            }
        }
    }
}
