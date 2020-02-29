using System;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
                
        public DbSet<Domain.Base> Bases { get; set; } = default!;
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

        
        
/*

dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool update --global dotnet-aspnet-codegenerator

in project folder:

dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
dotnet ef database update --project DAL.App.EF --startup-project WebApp
dotnet ef database drop --project DAL.App.EF --startup-project WebApp

in WebApp folder:

dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext -f

dotnet aspnet-codegenerator controller -name PersonController -actions -m Person -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name FriendController -actions -m Friend -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RestaurantController -actions -m Restaurant -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoryController -actions -m Category -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SizeController -actions -m Size -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name BaseController -actions -m Base -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MenuController -actions -m Menu -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MealController -actions -m Meal -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MenuMealController -actions -m MenuMeal -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MealPriceController -actions -m MealPrice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ComponentController -actions -m Component -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MealComponentController -actions -m MealComponent -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ComponentPriceController -actions -m ComponentPrice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserLocationController -actions -m UserLocation -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name HandoverTypeController -actions -m HandoverType -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CartController -actions -m Cart -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CartMealController -actions -m CartMeal -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PaymentMethodController -actions -m PaymentMethod -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name InvoiceController -actions -m Invoice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name InvoiceLineController -actions -m InvoiceLine -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SharingController -actions -m Sharing -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemController -actions -m Item -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SharingItemController -actions -m SharingItem -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ClientGroupController -actions -m ClientGroup -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserClientGroupController -actions -m UserClientGroup -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Base" asp-action="Index">Base</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Cart" asp-action="Index">Cart</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="CartMeal" asp-action="Index">CartMeal</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Category" asp-action="Index">Category</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="ClientGroup" asp-action="Index">ClientGroup</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Component" asp-action="Index">Component</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="ComponentPrice" asp-action="Index">ComponentPrice</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Friend" asp-action="Index">Friend</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="HandoverType" asp-action="Index">HandoverType</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Invoice" asp-action="Index">Invoice</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="InvoiceLine" asp-action="Index">InvoiceLine</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Item" asp-action="Index">Item</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Meal" asp-action="Index">Meal</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="MealComponent" asp-action="Index">MealComponent</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="MealPrice" asp-action="Index">MealPrice</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Menu" asp-action="Index">Menu</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="MenuMeal" asp-action="Index">MenuMeal</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="PaymentMethod" asp-action="Index">PaymentMethod</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Person" asp-action="Index">Person</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Restaurant" asp-action="Index">Restaurant</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Sharing" asp-action="Index">Sharing</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="SharingItem" asp-action="Index">SharingItem</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Size" asp-action="Index">Size</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="UserClientGroup" asp-action="Index">UserClientGroup</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="UserLocation" asp-action="Index">UserLocation</a>
                        </li>
*/
        
    }
}