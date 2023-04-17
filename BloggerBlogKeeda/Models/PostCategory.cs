using System.ComponentModel.DataAnnotations;

namespace BloggerBlogKeeda.Models
{
    public class PostCategory
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public Post? Post { get; set; }//ManytoMany with Foriegn Keys
        public Category? Category { get; set; }
    }
}
