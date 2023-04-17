namespace BloggerBlogKeeda.Models
{
    public class PostTags//ManytoMany
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int TagsId { get; set; }
        public Post? Post { get; set; }
        public Tags? Tags { get; set; }
    }
}
