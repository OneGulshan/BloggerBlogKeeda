namespace BloggerBlogKeeda.Models
{
    public class Tags
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public ICollection<PostTags>? PostTags { get; set; } = new HashSet<PostTags>();
    }
}
