using System.ComponentModel.DataAnnotations;

namespace BloggerBlogKeeda.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PublishedDate { get; set; }
        public ICollection<PostCategory>? PostCategories { get; set; } = new HashSet<PostCategory>(); //ManaytoMany //Ye Collection type prop jab bhi memory me aengi to kuch values lekar aengi islie ise HashSet<PostCategory> se initialize kia gaya hai yahan
        public AppUser? User { get; set; }
        public string? AppUserId { get; set; }//Default Identity me User Id Guid hoti hai isliye
        public PostStatus? StatusOfPost { get; set; }
        public bool PostVisibility { get; set; }//Showed When Published
        public ICollection<PostTags>? PostTags { get; set; } = new HashSet<PostTags>();
    }
    public enum PostStatus
    {
        Draft=0,
        Published=1,
        Scheduled=2
    }
}
