using System.ComponentModel.DataAnnotations;

namespace BloggerBlogKeeda.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PublishedDate { get; set; }
        public ICollection<PostCategory>? PostCategories { get; set; } = new HashSet<PostCategory>();
    }
}
