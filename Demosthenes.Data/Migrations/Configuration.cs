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
            AutomaticMigrationsEnabled = false;
            ContextKey = "Demosthenes.Data.DemosthenesContext";
        }

        protected override void Seed(Demosthenes.Data.DemosthenesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Departments.AddOrUpdate(d => d.Name,
                new Department { Name = "Computer Science" },
                new Department { Name = "Computer Enginner" },
                new Department { Name = "Psychology" }
            );
        }
    }
}
