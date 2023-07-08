using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniteds.CaseStudy.Domain.DTOs
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public int UserId { get; set; }
        public int? ParentId { get; set; }

        public bool IsDeleted { get; set; }

       
    }
}
