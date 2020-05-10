using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Domain.Base.EF.Repositories;
using Domain.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Domain.Base.App.EF.Repositories
{
    public class SharingItemRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.SharingItem, DTO.SharingItem>, 
        ISharingItemRepository
    {
        public SharingItemRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.SharingItem, DTO.SharingItem>())
        {
        }

        public async Task<IEnumerable<DTO.SharingItem>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(si => si.Item)
                .Include(si => si.Sharing)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(si => si.Sharing.AppUserId == userId);
            }

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.SharingItem> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(si => si.Item)
                .Include(si => si.Sharing)
                .Where(si => si.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(si => si.Sharing.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(cm => cm.Id == id);
            }

            return await RepoDbSet.AnyAsync(si => si.Sharing.AppUserId == userId);

        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var sharingItem = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(sharingItem.Id);
        }
        
        /*
        public async Task<IEnumerable<SharingItemDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(si => new SharingItemDTO()
                {
                    Id = si.Id,
                    ItemId = si.ItemId,
                    Item = si.Item == null
                        ? null
                        : new ItemDTO()
                        {
                            Id = si.Item.Id,
                            SharingId = si.Item.SharingId,
                            Sharing = new SharingDTO()
                            {
                                Id = si.Item.Sharing.Id,
                                AppUserId = si.Item.Sharing.AppUserId,
                                Name = si.Item.Sharing.Name
                            },
                            InvoiceLineId = si.Item.InvoiceLineId,
                            InvoiceLine = new InvoiceLineDTO()
                            {
                                Id = si.Item.InvoiceLine.Id,
                                CartId = si.Item.InvoiceLine.CartId,
                                Cart = new CartDTO()
                                {
                                    Id = si.Item.InvoiceLine.Cart.Id,
                                    AppUserId = si.Item.InvoiceLine.Cart.AppUser.Id,    // appUser
                                    AsDelivery = si.Item.InvoiceLine.Cart.AsDelivery,
                                    UserLocationId = si.Item.InvoiceLine.Cart.UserLocationId,
                                    UserLocation = si.Item.InvoiceLine.Cart.UserLocation == null
                                        ? null
                                        : new UserLocationDTO()
                                        {
                                            Id = si.Item.InvoiceLine.Cart.UserLocation.Id,
                                            AppUserId = si.Item.InvoiceLine.Cart.UserLocation.AppUser.Id,
                                            District = si.Item.InvoiceLine.Cart.UserLocation.District,
                                            StreetName = si.Item.InvoiceLine.Cart.UserLocation.StreetName,
                                            BuildingNumber = si.Item.InvoiceLine.Cart.UserLocation.BuildingNumber,
                                            ApartmentNumber = si.Item.InvoiceLine.Cart.UserLocation.ApartmentNumber
                                        },
                                    RestaurantId = si.Item.InvoiceLine.Cart.RestaurantId,
                                    Restaurant = new RestaurantDTO()
                                    {
                                        Id = si.Item.InvoiceLine.Cart.Restaurant.Id,
                                        Name = si.Item.InvoiceLine.Cart.Restaurant.Name,
                                        Location = si.Item.InvoiceLine.Cart.Restaurant.Location,
                                        Telephone = si.Item.InvoiceLine.Cart.Restaurant.Telephone,
                                        OpenTime = si.Item.InvoiceLine.Cart.Restaurant.OpenTime,
                                        OpenNotification = si.Item.InvoiceLine.Cart.Restaurant.OpenNotification
                                    },
                                    Total = si.Item.InvoiceLine.Cart.Total,
                                    ReadyBy = si.Item.InvoiceLine.Cart.ReadyBy
                                },
                                InvoiceId = si.Item.InvoiceLine.InvoiceId,
                                Invoice = new InvoiceDTO()
                                {
                                    Id = si.Item.InvoiceLine.Invoice.Id,
                                    PersonId = si.Item.InvoiceLine.Invoice.PersonId,
                                    Person = new PersonDTO()
                                    {
                                        Id = si.Item.InvoiceLine.Invoice.Person.Id,
                                        AppUserId = si.Item.InvoiceLine.Invoice.Person.AppUserId,
                                        ThisIsMe = si.Item.InvoiceLine.Invoice.Person.ThisIsMe,
                                        FirstName = si.Item.InvoiceLine.Invoice.Person.FirstName,
                                        LastName = si.Item.InvoiceLine.Invoice.Person.LastName,
                                        Phone = si.Item.InvoiceLine.Invoice.Person.Phone,
                                        NationalIdentificationNumber = si.Item.InvoiceLine.Invoice.Person.NationalIdentificationNumber,
                                        Since = si.Item.InvoiceLine.Invoice.Person.Since,
                                        Until = si.Item.InvoiceLine.Invoice.Person.Until,
                                    },
                                    RestaurantId = si.Item.InvoiceLine.Invoice.RestaurantId,
                                    Restaurant = new RestaurantDTO()
                                    {
                                        Id = si.Item.InvoiceLine.Invoice.Restaurant.Id,
                                        Name = si.Item.InvoiceLine.Invoice.Restaurant.Name,
                                        Location = si.Item.InvoiceLine.Invoice.Restaurant.Location,
                                        Telephone = si.Item.InvoiceLine.Invoice.Restaurant.Telephone,
                                        OpenTime = si.Item.InvoiceLine.Invoice.Restaurant.OpenTime,
                                        OpenNotification = si.Item.InvoiceLine.Invoice.Restaurant.OpenNotification
                                    },
                                    PaymentMethodId = si.Item.InvoiceLine.Invoice.PaymentMethodId,
                                    PaymentMethod = new PaymentMethodDTO()
                                    {
                                        Id = si.Item.InvoiceLine.Invoice.PaymentMethodId,
                                        Name = si.Item.InvoiceLine.Invoice.PaymentMethod.Name,
                                        Since = si.Item.InvoiceLine.Invoice.PaymentMethod.Since,
                                        Until = si.Item.InvoiceLine.Invoice.PaymentMethod.Until
                                    },
                                    TotalNet= si.Item.InvoiceLine.Invoice.TotalNet,
                                    TotalTax = si.Item.InvoiceLine.Invoice.TotalTax,
                                    TotalGross = si.Item.InvoiceLine.Invoice.TotalGross
                                }
                            },
                            Name = si.Item.Name,
                            Net = si.Item.Net,
                            Tax = si.Item.Tax,
                            Gross = si.Item.Gross,
                        },
                    SharingId = si.SharingId,
                    Sharing = si.Sharing == null
                        ? null
                        : new SharingDTO()
                        {
                            Id = si.Sharing.Id,
                            AppUserId = si.Sharing.AppUserId,
                            Name = si.Sharing.Name
                        },
                    FriendName = si.FriendName,
                    Percent = si.Percent,
                    FriendOwns = si.FriendOwns,
                })
                .ToListAsync();
        }

        public async Task<SharingItemDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable(); 
            SharingItemDTO sharingItemDTO = await query
                .Select(si => new SharingItemDTO()
                {
                    Id = si.Id,
                    ItemId = si.ItemId,
                    Item = si.Item == null
                        ? null
                        : new ItemDTO()
                        {
                            Id = si.Item.Id,
                            SharingId = si.Item.SharingId,
                            Sharing = new SharingDTO()
                            {
                                Id = si.Item.Sharing.Id,
                                AppUserId = si.Item.Sharing.AppUserId,
                                AppUser = si.Item.Sharing.AppUser,
                                Name = si.Item.Sharing.Name
                            },
                            InvoiceLineId = si.Item.InvoiceLineId,
                            InvoiceLine = new InvoiceLineDTO()
                            {
                                Id = si.Item.InvoiceLine.Id,
                                CartId = si.Item.InvoiceLine.CartId,
                                Cart = new CartDTO()
                                {
                                    Id = si.Item.InvoiceLine.Cart.Id,
                                    AppUserId = si.Item.InvoiceLine.Cart.AppUser.Id,    // appUser
                                    AsDelivery = si.Item.InvoiceLine.Cart.AsDelivery,
                                    UserLocationId = si.Item.InvoiceLine.Cart.UserLocationId,
                                    UserLocation = si.Item.InvoiceLine.Cart.UserLocation == null
                                        ? null
                                        : new UserLocationDTO()
                                        {
                                            Id = si.Item.InvoiceLine.Cart.UserLocation.Id,
                                            AppUserId = si.Item.InvoiceLine.Cart.UserLocation.AppUser.Id,
                                            District = si.Item.InvoiceLine.Cart.UserLocation.District,
                                            StreetName = si.Item.InvoiceLine.Cart.UserLocation.StreetName,
                                            BuildingNumber = si.Item.InvoiceLine.Cart.UserLocation.BuildingNumber,
                                            ApartmentNumber = si.Item.InvoiceLine.Cart.UserLocation.ApartmentNumber
                                        },
                                    RestaurantId = si.Item.InvoiceLine.Cart.RestaurantId,
                                    Restaurant = new RestaurantDTO()
                                    {
                                        Id = si.Item.InvoiceLine.Cart.Restaurant.Id,
                                        Name = si.Item.InvoiceLine.Cart.Restaurant.Name,
                                        Location = si.Item.InvoiceLine.Cart.Restaurant.Location,
                                        Telephone = si.Item.InvoiceLine.Cart.Restaurant.Telephone,
                                        OpenTime = si.Item.InvoiceLine.Cart.Restaurant.OpenTime,
                                        OpenNotification = si.Item.InvoiceLine.Cart.Restaurant.OpenNotification
                                    },
                                    Total = si.Item.InvoiceLine.Cart.Total,
                                    ReadyBy = si.Item.InvoiceLine.Cart.ReadyBy
                                },
                                InvoiceId = si.Item.InvoiceLine.InvoiceId,
                                Invoice = new InvoiceDTO()
                                {
                                    Id = si.Item.InvoiceLine.Invoice.Id,
                                    PersonId = si.Item.InvoiceLine.Invoice.PersonId,
                                    Person = new PersonDTO()
                                    {
                                        Id = si.Item.InvoiceLine.Invoice.Person.Id,
                                        AppUserId = si.Item.InvoiceLine.Invoice.Person.AppUserId,
                                        ThisIsMe = si.Item.InvoiceLine.Invoice.Person.ThisIsMe,
                                        FirstName = si.Item.InvoiceLine.Invoice.Person.FirstName,
                                        LastName = si.Item.InvoiceLine.Invoice.Person.LastName,
                                        Phone = si.Item.InvoiceLine.Invoice.Person.Phone,
                                        NationalIdentificationNumber = si.Item.InvoiceLine.Invoice.Person.NationalIdentificationNumber,
                                        Since = si.Item.InvoiceLine.Invoice.Person.Since,
                                        Until = si.Item.InvoiceLine.Invoice.Person.Until,
                                    },
                                    RestaurantId = si.Item.InvoiceLine.Invoice.RestaurantId,
                                    Restaurant = new RestaurantDTO()
                                    {
                                        Id = si.Item.InvoiceLine.Invoice.Restaurant.Id,
                                        Name = si.Item.InvoiceLine.Invoice.Restaurant.Name,
                                        Location = si.Item.InvoiceLine.Invoice.Restaurant.Location,
                                        Telephone = si.Item.InvoiceLine.Invoice.Restaurant.Telephone,
                                        OpenTime = si.Item.InvoiceLine.Invoice.Restaurant.OpenTime,
                                        OpenNotification = si.Item.InvoiceLine.Invoice.Restaurant.OpenNotification
                                    },
                                    PaymentMethodId = si.Item.InvoiceLine.Invoice.PaymentMethodId,
                                    PaymentMethod = new PaymentMethodDTO()
                                    {
                                        Id = si.Item.InvoiceLine.Invoice.PaymentMethodId,
                                        Name = si.Item.InvoiceLine.Invoice.PaymentMethod.Name,
                                        Since = si.Item.InvoiceLine.Invoice.PaymentMethod.Since,
                                        Until = si.Item.InvoiceLine.Invoice.PaymentMethod.Until
                                    },
                                    TotalNet= si.Item.InvoiceLine.Invoice.TotalNet,
                                    TotalTax = si.Item.InvoiceLine.Invoice.TotalTax,
                                    TotalGross = si.Item.InvoiceLine.Invoice.TotalGross
                                }
                            },
                            Name = si.Item.Name,
                            Net = si.Item.Net,
                            Tax = si.Item.Tax,
                            Gross = si.Item.Gross,
                        },
                    SharingId = si.SharingId,
                    Sharing = si.Sharing == null
                        ? null
                        : new SharingDTO()
                        {
                            Id = si.Sharing.Id,
                            AppUserId = si.Sharing.AppUserId,
                            Name = si.Sharing.Name
                        },
                    FriendName = si.FriendName,
                    Percent = si.Percent,
                    FriendOwns = si.FriendOwns,
                })
                .FirstOrDefaultAsync();
            
            return sharingItemDTO;
        }
        */
    }
}