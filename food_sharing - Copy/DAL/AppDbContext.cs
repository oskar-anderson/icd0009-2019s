using System;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Component = System.ComponentModel.Component;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartMeal> CartMeals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<HandoverType> HandoverTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealComponent> MealComponents { get; set; }
        public DbSet<MealTag> MealTags { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentOption> PaymentOptions { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Sharing> Sharings { get; set; }
        public DbSet<SharingItem> SharingItems { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFav> UserFavs { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }

        /*
        
        dotnet tool install --global dotnet-aspnet-codegenerator
        dotnet tool update --global dotnet-aspnet-codegenerator

        in project folder:
        dotnet ef migrations add InitialDbCreation --project WebApp --startup-project WebApp
        dotnet ef database update --project WebApp --startup-project WebApp

        in WebApp folder:
        dotnet aspnet-codegenerator controller -name UserController -actions -m User -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name RestaurantController -actions -m Restaurant -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name MenuController -actions -m Menu -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserFriendController -actions -m UserFriend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        
        
        */
        
        
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}