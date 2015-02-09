
using Demosthenes.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Demosthenes.Core
{
    public class Post : IDateTrackable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4), MaxLength(128)]
        public string Title { get; set; }
        [Required]
        [MinLength(8), MaxLength(1024)]
        public string Content { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
