using System;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        
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
        public DbSet<UserClientGroup> UserClientGroups { get; set; } = default!;
        public DbSet<UserLocation> UserLocations { get; set; } = default!;

    
    }
}