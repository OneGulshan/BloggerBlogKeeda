using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BloggerBlogKeeda.ViewModels
{
    public class PostViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime? PublishedDate { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public List<SelectListItem>? Tags { get; set; }
    }
}
