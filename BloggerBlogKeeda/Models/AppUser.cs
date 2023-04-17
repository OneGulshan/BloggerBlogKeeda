using Microsoft.AspNetCore.Identity;

namespace BloggerBlogKeeda.Models
{
    public class AppUser : IdentityUser
    {
        public string? PictureURL { get; set; }
    }
}
