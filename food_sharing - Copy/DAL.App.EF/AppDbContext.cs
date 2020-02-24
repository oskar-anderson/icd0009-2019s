using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : DbContext
    {
        protected internal static DbContextOptions<AppDbContext> Options;
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Options = options;
        }

        
        public DbSet<Base> Bases { get; set; } = default!;
        public DbSet<Cart> Carts { get; set; } = default!;
        public DbSet<CartMeal> CartMeals { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<ClientGroup> ClientGroups { get; set; } = default!;
        public DbSet<Component> Components { get; set; } = default!;
        public DbSet<Friend> Friends { get; set; } = default!;
        public DbSet<ComponentPrice> ComponentPrices { get; set; } = default!;
        public DbSet<HandoverType> HandoverTypes { get; set; } = default!;
        public DbSet<Invoice> Invoices { get; set; } = default!;
        public DbSet<InvoiceLine> InvoiceLines { get; set; } = default!;
        public DbSet<Item> Items { get; set; } = default!;
        public DbSet<Meal> Meals { get; set; } = default!;
        public DbSet<MealComponent> MealComponents { get; set; } = default!;
        public DbSet<MealPrice> MealPrices { get; set; } = default!;
        public DbSet<Menu> Menus { get; set; } = default!;
        public DbSet<MenuMeal> MenuMeals { get; set; } = default!;
        public DbSet<PaymentMethod> PaymentOptions { get; set; } = default!;
        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Restaurant> Restaurants { get; set; } = default!;
        public DbSet<Sharing> Sharings { get; set; } = default!;
        public DbSet<SharingItem> SharingItems { get; set; } = default!;
        public DbSet<Size> Sizes { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<UserClientGroup> UserClientGroups { get; set; } = default!;
        public DbSet<UserLocation> UserLocations { get; set; } = default!;

        /*
        
        dotnet tool install --global dotnet-aspnet-codegenerator
        dotnet tool update --global dotnet-aspnet-codegenerator

        in project folder:
        dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
        dotnet ef database update --project DAL.App.EF --startup-project WebApp

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
        
        
        
        
    }
}