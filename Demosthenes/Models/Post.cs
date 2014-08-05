﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demosthenes.Models
{
    public class Post : Base.TimeStampsEntity
    {
        public Post()
            : base()
        {
            Visible = false;
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "PostTitle", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public string Title { get; set; }
        
        [Display(Name = "PostBody", ResourceType = typeof(Resources.i18n.Models))]
        [Required]
        public string Body { get; set; }

        [ScaffoldColumn(false)]
        public bool Visible { get; set; }

        [Display(Name = "PostAuthorId", ResourceType = typeof(Resources.i18n.Models))]
        [Required, ForeignKey("Author")]
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }
}