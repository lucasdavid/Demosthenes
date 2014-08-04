using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demosthenes.Models.Base
{
    public class TimeStampsEntity
    {
        TimeStampsEntity()
        {
            DateCreated = DateTime.Now;
        }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}