using Demosthenes.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demosthenes.Core
{
    public class Department : IDateTrackable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public DateTime? DateDeleted { get; set; }
    }
}
