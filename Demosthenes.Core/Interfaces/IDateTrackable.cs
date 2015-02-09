using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosthenes.Core.Interfaces
{
    public interface IDateTrackable
    {
        DateTime DateCreated { get; set; }
        DateTime? DateUpdated { get; set; }
    }
}
