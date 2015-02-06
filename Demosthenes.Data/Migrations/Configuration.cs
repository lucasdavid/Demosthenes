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
            try
            {
                context.Schedules.AddOrUpdate(s => s.Id,
                    new Schedule { TimeStarted = new TimeSpan(8, 0, 0), TimeFinished = new TimeSpan(10, 0, 0) },
                    new Schedule { TimeStarted = new TimeSpan(10, 0, 0), TimeFinished = new TimeSpan(12, 0, 0) },
                    new Schedule { TimeStarted = new TimeSpan(14, 0, 0), TimeFinished = new TimeSpan(16, 0, 0) },
                    new Schedule { TimeStarted = new TimeSpan(16, 0, 0), TimeFinished = new TimeSpan(18, 0, 0) },
                    new Schedule { TimeStarted = new TimeSpan(19, 0, 0), TimeFinished = new TimeSpan(21, 0, 0) },
                    new Schedule { TimeStarted = new TimeSpan(21, 0, 0), TimeFinished = new TimeSpan(23, 0, 0) }
                );
            }
            catch (Exception)
            {
                //
            }
        }
    }
}
