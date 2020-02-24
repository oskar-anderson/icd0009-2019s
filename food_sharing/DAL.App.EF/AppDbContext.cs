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
        dotnet aspnet-codegenerator controller -name PersonController -actions -m Person -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserController -actions -m User -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name FriendController -actions -m Friend -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name RestaurantController -actions -m Restaurant -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name CategoryController -actions -m Category -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name SizeController -actions -m Size -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name BaseController -actions -m Base -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name MenuController -actions -m Menu -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name MealController -actions -m Meal -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name MenuMealController -actions -m MenuMeal -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name MealPriceController -actions -m MealPrice -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name ComponentController -actions -m Component -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name MealComponentController -actions -m MealComponent -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name ComponentPriceController -actions -m ComponentPrice -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserLocationController -actions -m UserLocation -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name HandoverTypeController -actions -m HandoverType -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name CartController -actions -m Cart -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name CartMealController -actions -m CartMeal -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name PaymentMethodController -actions -m PaymentMethod -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name InvoiceController -actions -m Invoice -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name InvoiceLineController -actions -m InvoiceLine -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name SharingController -actions -m Sharing -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name ItemController -actions -m Item -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name SharingItemController -actions -m SharingItem -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name RoleController -actions -m Role -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserRoleController -actions -m UserRole -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name ClientGroupController -actions -m ClientGroup -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name UserClientGroupController -actions -m UserClientGroup -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        
        
        */
        
    }
}