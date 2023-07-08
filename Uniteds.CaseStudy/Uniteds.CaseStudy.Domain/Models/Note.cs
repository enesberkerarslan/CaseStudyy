using System;
using System.Collections.Generic;

namespace Uniteds.CaseStudy.Domain.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int? ParentId { get; set; }
        public Note ParentNote { get; set; }
        public User user { get; set; }
        public int UserId { get; set; }
        public ICollection<Note> Children { get; set; }
        public bool IsDeleted { get; set; } // Soft delete için özellik

    }
}
