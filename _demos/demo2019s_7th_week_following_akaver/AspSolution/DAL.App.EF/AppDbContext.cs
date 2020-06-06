using System;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, string>
    {
        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<Post> Posts { get; set; }  = default!;
        public DbSet<AuthorPicture> AuthorPictures { get; set; }  = default!;
        public DbSet<Category> Categories { get; set; }  = default!;
        public DbSet<PostCategory> PostCategories { get; set; } = default!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}