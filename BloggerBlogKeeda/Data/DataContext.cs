using BloggerBlogKeeda.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloggerBlogKeeda.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){ }//Here Making our DbSet Tables in db using by DbContextOptions service thats using in program.cs file.
        //public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Post>? Post { get; set; }
        public DbSet<Category>? Category { get; set; }
        public DbSet<PostCategory>? PostCategory { get; set; }
        public DbSet<Tags>? Tags { get; set; }
        public DbSet<PostTags>? PostTags { get; set; }
    }
}
