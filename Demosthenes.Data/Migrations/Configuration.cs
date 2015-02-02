namespace Demosthenes.Data.Migrations
{
    using Demosthenes.Core;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Demosthenes.Data.DemosthenesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Demosthenes.Data.DemosthenesContext context)
        {
            context.Departments.AddOrUpdate(d => d.Name,
                new Department { Name = "Computer Science" },
                new Department { Name = "Computer Enginner" },
                new Department { Name = "Psychology" },
                new Department { Name = "Biology" },
                new Department { Name = "Geography" });
        }
    }
}
