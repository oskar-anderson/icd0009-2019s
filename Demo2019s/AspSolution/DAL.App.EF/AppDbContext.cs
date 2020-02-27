using System;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<Post> Posts { get; set; } = default!;
        public DbSet<AuthorPicture> AuthorPictures { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<PostCategory> PostCategories { get; set; } = default!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        /*
         
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool update --global dotnet-aspnet-codegenerator

in project folder:
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
dotnet ef database update --project DAL.App.EF --startup-project WebApp
dotnet ef database drop --project DAL.App.EF --startup-project WebApp

dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext -f
        
in WebApp:
dotnet aspnet-codegenerator controller -name AuthorsController -actions -m Author -dc ./DAL.App.EF/ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoriesController -actions -m Category -dc ./DAL.App.EF/ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AuthorPicturesController -actions -m AuthorPicture -dc ./DAL.App.EF/ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PostsController -actions -m Post -dc ./DAL.App.EF/ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PostCategoriesController -actions -m PostCategories -dc ./DAL.App.EF/ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AuthorsController -actions -m Author -dc ./DAL.App.EF/ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f



        
        */
    }
}