namespace Uniteds.CaseStudy.Web.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int? ParentId { get; set; }
        public bool IsDeleted { get; set; }
        public List<Note> Children { get; set; }
    }
}
