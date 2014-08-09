using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demosthenes.Core.Models.Base
{
    public abstract class TimeStampsEntity
    {
        public TimeStampsEntity()
        {
            DateCreated = DateTime.Now;
        }

        [Required, ScaffoldColumn(false)]
        public DateTime DateCreated { get; private set; }
    }
}