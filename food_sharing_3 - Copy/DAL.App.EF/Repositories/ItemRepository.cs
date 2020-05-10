using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App.Identity;
using Domain.Base.App.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ItemRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Item, Item>, 
        IItemRepository
    {
        public ItemRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.Item, Item>())
        {
        }

        public async Task<IEnumerable<Item>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(i => i.InvoiceLine)
                .Include(i => i.Sharing)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(i => i.Sharing.AppUser.Id == userId);
            }

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<Item> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(i => i.InvoiceLine)
                .Include(i => i.Sharing)
                .Where(i => i.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(i => i.Sharing.AppUser.Id == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(i => i.Id == id);
            }

            return await RepoDbSet.AnyAsync(i => i.Sharing.AppUser.Id == userId);

        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var item = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(item.Id);
        }
        /*
        public async Task<IEnumerable<ItemDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(i => new ItemDTO()
                {
                    Id = i.Id,
                    SharingId = i.SharingId,
                    Sharing = new SharingDTO()
                    {
                        Id = i.Sharing.Id,
                        AppUserId = i.Sharing.AppUserId,
                        Name = i.Sharing.Name
                    },
                    InvoiceLineId = i.InvoiceLineId,
                    InvoiceLine = new InvoiceLineDTO()
                    {
                        Id = i.InvoiceLine.Id,
                        CartId = i.InvoiceLine.CartId,
                        Cart = new CartDTO()
                        {
                            Id = i.InvoiceLine.Cart.Id,
                            AppUserId = i.InvoiceLine.Cart.AppUser.Id,    // appUser
                            AsDelivery = i.InvoiceLine.Cart.AsDelivery,
                            UserLocationId = i.InvoiceLine.Cart.UserLocationId,
                            UserLocation = i.InvoiceLine.Cart.UserLocation == null
                                ? null
                                : new UserLocationDTO()
                                {
                                    Id = i.InvoiceLine.Cart.UserLocation.Id,
                                    AppUserId = i.InvoiceLine.Cart.UserLocation.AppUser.Id,
                                    District = i.InvoiceLine.Cart.UserLocation.District,
                                    StreetName = i.InvoiceLine.Cart.UserLocation.StreetName,
                                    BuildingNumber = i.InvoiceLine.Cart.UserLocation.BuildingNumber,
                                    ApartmentNumber = i.InvoiceLine.Cart.UserLocation.ApartmentNumber
                                },
                            RestaurantId = i.InvoiceLine.Cart.RestaurantId,
                            Restaurant = new RestaurantDTO()
                            {
                                Id = i.InvoiceLine.Cart.Restaurant.Id,
                                Name = i.InvoiceLine.Cart.Restaurant.Name,
                                Location = i.InvoiceLine.Cart.Restaurant.Location,
                                Telephone = i.InvoiceLine.Cart.Restaurant.Telephone,
                                OpenTime = i.InvoiceLine.Cart.Restaurant.OpenTime,
                                OpenNotification = i.InvoiceLine.Cart.Restaurant.OpenNotification
                            },
                            Total = i.InvoiceLine.Cart.Total,
                            ReadyBy = i.InvoiceLine.Cart.ReadyBy
                        },
                        InvoiceId = i.InvoiceLine.InvoiceId,
                        Invoice = new InvoiceDTO()
                        {
                            Id = i.InvoiceLine.Invoice.Id,
                            PersonId = i.InvoiceLine.Invoice.PersonId,
                            Person = new PersonDTO()
                            {
                                Id = i.InvoiceLine.Invoice.Person.Id,
                                AppUserId = i.InvoiceLine.Invoice.Person.AppUserId,
                                ThisIsMe = i.InvoiceLine.Invoice.Person.ThisIsMe,
                                FirstName = i.InvoiceLine.Invoice.Person.FirstName,
                                LastName = i.InvoiceLine.Invoice.Person.LastName,
                                Phone = i.InvoiceLine.Invoice.Person.Phone,
                                NationalIdentificationNumber = i.InvoiceLine.Invoice.Person.NationalIdentificationNumber,
                                Since = i.InvoiceLine.Invoice.Person.Since,
                                Until = i.InvoiceLine.Invoice.Person.Until,
                            },
                            RestaurantId = i.InvoiceLine.Invoice.RestaurantId,
                            Restaurant = new RestaurantDTO()
                            {
                                Id = i.InvoiceLine.Invoice.Restaurant.Id,
                                Name = i.InvoiceLine.Invoice.Restaurant.Name,
                                Location = i.InvoiceLine.Invoice.Restaurant.Location,
                                Telephone = i.InvoiceLine.Invoice.Restaurant.Telephone,
                                OpenTime = i.InvoiceLine.Invoice.Restaurant.OpenTime,
                                OpenNotification = i.InvoiceLine.Invoice.Restaurant.OpenNotification
                            },
                            PaymentMethodId = i.InvoiceLine.Invoice.PaymentMethodId,
                            PaymentMethod = new PaymentMethodDTO()
                            {
                                Id = i.InvoiceLine.Invoice.PaymentMethodId,
                                Name = i.InvoiceLine.Invoice.PaymentMethod.Name,
                                Since = i.InvoiceLine.Invoice.PaymentMethod.Since,
                                Until = i.InvoiceLine.Invoice.PaymentMethod.Until
                            },
                            TotalNet= i.InvoiceLine.Invoice.TotalNet,
                            TotalTax = i.InvoiceLine.Invoice.TotalTax,
                            TotalGross = i.InvoiceLine.Invoice.TotalGross
                        }
                    },
                    Name = i.Name,
                    Net = i.Net,
                    Tax = i.Tax,
                    Gross = i.Gross,
                    
                })
                .ToListAsync();
        }

        public async Task<ItemDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(i => i.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(i => i.Sharing.AppUser.Id == userId);
            }
            
            return await query
                .Select(i => new ItemDTO()
                {
                    Id = i.Id,
                    SharingId = i.SharingId,
                    Sharing = new SharingDTO()
                    {
                        Id = i.Sharing.Id,
                        AppUserId = i.Sharing.AppUserId,
                        Name = i.Sharing.Name
                    },
                    InvoiceLineId = i.InvoiceLineId,
                    InvoiceLine = new InvoiceLineDTO()
                    {
                        Id = i.InvoiceLine.Id,
                        CartId = i.InvoiceLine.CartId,
                        Cart = new CartDTO()
                        {
                            Id = i.InvoiceLine.Cart.Id,
                            AppUserId = i.InvoiceLine.Cart.AppUser.Id,    // appUser
                            AsDelivery = i.InvoiceLine.Cart.AsDelivery,
                            UserLocationId = i.InvoiceLine.Cart.UserLocationId,
                            UserLocation = i.InvoiceLine.Cart.UserLocation == null
                                ? null
                                : new UserLocationDTO()
                                {
                                    Id = i.InvoiceLine.Cart.UserLocation.Id,
                                    AppUserId = i.InvoiceLine.Cart.UserLocation.AppUser.Id,
                                    District = i.InvoiceLine.Cart.UserLocation.District,
                                    StreetName = i.InvoiceLine.Cart.UserLocation.StreetName,
                                    BuildingNumber = i.InvoiceLine.Cart.UserLocation.BuildingNumber,
                                    ApartmentNumber = i.InvoiceLine.Cart.UserLocation.ApartmentNumber
                                },
                            RestaurantId = i.InvoiceLine.Cart.RestaurantId,
                            Restaurant = new RestaurantDTO()
                            {
                                Id = i.InvoiceLine.Cart.Restaurant.Id,
                                Name = i.InvoiceLine.Cart.Restaurant.Name,
                                Location = i.InvoiceLine.Cart.Restaurant.Location,
                                Telephone = i.InvoiceLine.Cart.Restaurant.Telephone,
                                OpenTime = i.InvoiceLine.Cart.Restaurant.OpenTime,
                                OpenNotification = i.InvoiceLine.Cart.Restaurant.OpenNotification
                            },
                            Total = i.InvoiceLine.Cart.Total,
                            ReadyBy = i.InvoiceLine.Cart.ReadyBy
                        },
                        InvoiceId = i.InvoiceLine.InvoiceId,
                        Invoice = new InvoiceDTO()
                        {
                            Id = i.InvoiceLine.Invoice.Id,
                            PersonId = i.InvoiceLine.Invoice.PersonId,
                            Person = new PersonDTO()
                            {
                                Id = i.InvoiceLine.Invoice.Person.Id,
                                AppUserId = i.InvoiceLine.Invoice.Person.AppUserId,
                                ThisIsMe = i.InvoiceLine.Invoice.Person.ThisIsMe,
                                FirstName = i.InvoiceLine.Invoice.Person.FirstName,
                                LastName = i.InvoiceLine.Invoice.Person.LastName,
                                Phone = i.InvoiceLine.Invoice.Person.Phone,
                                NationalIdentificationNumber = i.InvoiceLine.Invoice.Person.NationalIdentificationNumber,
                                Since = i.InvoiceLine.Invoice.Person.Since,
                                Until = i.InvoiceLine.Invoice.Person.Until,
                            },
                            RestaurantId = i.InvoiceLine.Invoice.RestaurantId,
                            Restaurant = new RestaurantDTO()
                            {
                                Id = i.InvoiceLine.Invoice.Restaurant.Id,
                                Name = i.InvoiceLine.Invoice.Restaurant.Name,
                                Location = i.InvoiceLine.Invoice.Restaurant.Location,
                                Telephone = i.InvoiceLine.Invoice.Restaurant.Telephone,
                                OpenTime = i.InvoiceLine.Invoice.Restaurant.OpenTime,
                                OpenNotification = i.InvoiceLine.Invoice.Restaurant.OpenNotification
                            },
                            PaymentMethodId = i.InvoiceLine.Invoice.PaymentMethodId,
                            PaymentMethod = new PaymentMethodDTO()
                            {
                                Id = i.InvoiceLine.Invoice.PaymentMethodId,
                                Name = i.InvoiceLine.Invoice.PaymentMethod.Name,
                                Since = i.InvoiceLine.Invoice.PaymentMethod.Since,
                                Until = i.InvoiceLine.Invoice.PaymentMethod.Until
                            },
                            TotalNet= i.InvoiceLine.Invoice.TotalNet,
                            TotalTax = i.InvoiceLine.Invoice.TotalTax,
                            TotalGross = i.InvoiceLine.Invoice.TotalGross
                        }
                    },
                    Name = i.Name,
                    Net = i.Net,
                    Tax = i.Tax,
                    Gross = i.Gross,
                }).FirstOrDefaultAsync();
        }
        */
    }
}