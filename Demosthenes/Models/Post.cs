using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Models
{
    public class Post
    {
        public Post()
        {
            DateCreated = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required, ForeignKey("Author")]
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public DateTime DateCreated { get; protected set; }
    }
}