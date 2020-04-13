using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<Cart> Carts { get; set; } = default!;
        public DbSet<CartMeal> CartMeals { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Component> Components { get; set; } = default!;
        public DbSet<ComponentPrice> ComponentPrices { get; set; } = default!;
        public DbSet<Invoice> Invoices { get; set; } = default!;
        public DbSet<InvoiceLine> InvoiceLines { get; set; } = default!;
        public DbSet<Item> Items { get; set; } = default!;
        public DbSet<Meal> Meals { get; set; } = default!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Pizza> Pizzas { get; set; } = default!;
        public DbSet<PizzaComponent> PizzaComponents { get; set; } = default!;
        public DbSet<PizzaFinal> PizzaFinals { get; set; } = default!;
        public DbSet<PizzaTemplate> PizzaTemplates { get; set; } = default!;
        public DbSet<Restaurant> Restaurants { get; set; } = default!;
        public DbSet<RestaurantFood> RestaurantFoods { get; set; } = default!;
        public DbSet<Sharing> Sharings { get; set; } = default!;
        public DbSet<SharingItem> SharingItems { get; set; } = default!;
        public DbSet<Size> Sizes { get; set; } = default!;
        public DbSet<UserLocation> UserLocations { get; set; } = default!;
        
        
        
        private IUserNameProvider _userNameProvider;
        public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameProvider userNameProvider)
            : base(options)
        {
            _userNameProvider = userNameProvider;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.
                GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private void SaveChangesMetadataUpdate()
        {
            // update the state of ef tracked objects
            ChangeTracker.DetectChanges();
            
            var markedAsAdded = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entityEntry in markedAsAdded)
            {
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.CreatedAt = DateTime.Now;
                entityWithMetaData.CreatedBy = _userNameProvider.CurrentUserName;
                entityWithMetaData.ChangedAt = entityWithMetaData.CreatedAt;
                entityWithMetaData.ChangedBy = entityWithMetaData.CreatedBy;
            }

            var markedAsModified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entityEntry in markedAsModified)
            {
                // check for IDomainEntityMetadata
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.ChangedAt = DateTime.Now;
                entityWithMetaData.ChangedBy = _userNameProvider.CurrentUserName;

                // do not let changes on these properties get into generated db sentences - db keeps old values
                entityEntry.Property(nameof(entityWithMetaData.CreatedAt)).IsModified = false;
                entityEntry.Property(nameof(entityWithMetaData.CreatedBy)).IsModified = false;
            }
        }

        public override int SaveChanges()
        {
            SaveChangesMetadataUpdate();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SaveChangesMetadataUpdate();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}