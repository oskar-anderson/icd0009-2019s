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
    public class InvoiceLineRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.InvoiceLine, DTO.InvoiceLine>, 
        IInvoiceLineRepository
    {
        public InvoiceLineRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.InvoiceLine, DTO.InvoiceLine>())
        {
        }

        public async Task<IEnumerable<DTO.InvoiceLine>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(a => a.Invoice)
                .Include(a => a.Cart)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(il => il.Invoice.Person.AppUser.Id == userId);
            }

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.InvoiceLine> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Invoice)
                .Include(a => a.Cart)
                .Where(a => a.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(il => il.Invoice.Person.AppUser.Id == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(il => il.Id == id);
            }

            return await RepoDbSet.AnyAsync(il => il.Invoice.Person.AppUser.Id == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var invoiceLine = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(invoiceLine.Id);
        }

        /*
        public async Task<IEnumerable<InvoiceLineDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(il => il.Invoice.Person.AppUser.Id == userId);
            }

            return await query
                .Select(il => new InvoiceLineDTO()
                {
                    Id = il.Id,
                    CartId = il.CartId,
                    Cart = new CartDTO()
                    {
                        Id = il.Cart.Id,
                        AppUserId = il.Cart.AppUser.Id,    // appUser
                        AsDelivery = il.Cart.AsDelivery,
                        UserLocationId = il.Cart.UserLocationId,
                        UserLocation = il.Cart.UserLocation == null
                            ? null
                            : new UserLocationDTO()
                            {
                                Id = il.Cart.UserLocation.Id,
                                AppUserId = il.Cart.UserLocation.AppUser.Id,
                                District = il.Cart.UserLocation.District,
                                StreetName = il.Cart.UserLocation.StreetName,
                                BuildingNumber = il.Cart.UserLocation.BuildingNumber,
                                ApartmentNumber = il.Cart.UserLocation.ApartmentNumber
                            },
                        RestaurantId = il.Cart.RestaurantId,
                        Restaurant = new RestaurantDTO()
                        {
                            Id = il.Cart.Restaurant.Id,
                            Name = il.Cart.Restaurant.Name,
                            Location = il.Cart.Restaurant.Location,
                            Telephone = il.Cart.Restaurant.Telephone,
                            OpenTime = il.Cart.Restaurant.OpenTime,
                            OpenNotification = il.Cart.Restaurant.OpenNotification
                        },
                        Total = il.Cart.Total,
                        ReadyBy = il.Cart.ReadyBy
                    },
                    InvoiceId = il.InvoiceId,
                    Invoice = new InvoiceDTO()
                    {
                        Id = il.Invoice.Id,
                        PersonId = il.Invoice.PersonId,
                        Person = new PersonDTO()
                        {
                            Id = il.Invoice.Person.Id,
                            AppUserId = il.Invoice.Person.AppUserId,
                            ThisIsMe = il.Invoice.Person.ThisIsMe,
                            FirstName = il.Invoice.Person.FirstName,
                            LastName = il.Invoice.Person.LastName,
                            Phone = il.Invoice.Person.Phone,
                            NationalIdentificationNumber = il.Invoice.Person.NationalIdentificationNumber,
                            Since = il.Invoice.Person.Since,
                            Until = il.Invoice.Person.Until,
                        },
                        RestaurantId = il.Invoice.RestaurantId,
                        Restaurant = new RestaurantDTO()
                        {
                            Id = il.Invoice.Restaurant.Id,
                            Name = il.Invoice.Restaurant.Name,
                            Location = il.Invoice.Restaurant.Location,
                            Telephone = il.Invoice.Restaurant.Telephone,
                            OpenTime = il.Invoice.Restaurant.OpenTime,
                            OpenNotification = il.Invoice.Restaurant.OpenNotification
                        },
                        PaymentMethodId = il.Invoice.PaymentMethodId,
                        PaymentMethod = new PaymentMethodDTO()
                        {
                            Id = il.Invoice.PaymentMethodId,
                            Name = il.Invoice.PaymentMethod.Name,
                            Since = il.Invoice.PaymentMethod.Since,
                            Until = il.Invoice.PaymentMethod.Until
                        },
                        TotalNet= il.Invoice.TotalNet,
                        TotalTax = il.Invoice.TotalTax,
                        TotalGross = il.Invoice.TotalGross
                    }
                })
                .ToListAsync();
        }

        public async Task<InvoiceLineDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(il => il.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(il => il.Invoice.Person.AppUser.Id == userId);
            }
            
            return await query
                .Select(il => new InvoiceLineDTO()
                {
                    Id = il.Id,
                    CartId = il.CartId,
                    Cart = new CartDTO()
                    {
                        Id = il.Cart.Id,
                        AppUserId = il.Cart.AppUser.Id,    // appUser
                        AsDelivery = il.Cart.AsDelivery,
                        UserLocationId = il.Cart.UserLocationId,
                        UserLocation = il.Cart.UserLocation == null
                            ? null
                            : new UserLocationDTO()
                            {
                                Id = il.Cart.UserLocation.Id,
                                AppUserId = il.Cart.UserLocation.AppUser.Id,
                                District = il.Cart.UserLocation.District,
                                StreetName = il.Cart.UserLocation.StreetName,
                                BuildingNumber = il.Cart.UserLocation.BuildingNumber,
                                ApartmentNumber = il.Cart.UserLocation.ApartmentNumber
                            },
                        RestaurantId = il.Cart.RestaurantId,
                        Restaurant = new RestaurantDTO()
                        {
                            Id = il.Cart.Restaurant.Id,
                            Name = il.Cart.Restaurant.Name,
                            Location = il.Cart.Restaurant.Location,
                            Telephone = il.Cart.Restaurant.Telephone,
                            OpenTime = il.Cart.Restaurant.OpenTime,
                            OpenNotification = il.Cart.Restaurant.OpenNotification
                        },
                        Total = il.Cart.Total,
                        ReadyBy = il.Cart.ReadyBy
                    },
                    InvoiceId = il.InvoiceId,
                    Invoice = new InvoiceDTO()
                    {
                        Id = il.Invoice.Id,
                        PersonId = il.Invoice.PersonId,
                        Person = new PersonDTO()
                        {
                            Id = il.Invoice.Person.Id,
                            AppUserId = il.Invoice.Person.AppUserId,
                            ThisIsMe = il.Invoice.Person.ThisIsMe,
                            FirstName = il.Invoice.Person.FirstName,
                            LastName = il.Invoice.Person.LastName,
                            Phone = il.Invoice.Person.Phone,
                            NationalIdentificationNumber = il.Invoice.Person.NationalIdentificationNumber,
                            Since = il.Invoice.Person.Since,
                            Until = il.Invoice.Person.Until,
                        },
                        RestaurantId = il.Invoice.RestaurantId,
                        Restaurant = new RestaurantDTO()
                        {
                            Id = il.Invoice.Restaurant.Id,
                            Name = il.Invoice.Restaurant.Name,
                            Location = il.Invoice.Restaurant.Location,
                            Telephone = il.Invoice.Restaurant.Telephone,
                            OpenTime = il.Invoice.Restaurant.OpenTime,
                            OpenNotification = il.Invoice.Restaurant.OpenNotification
                        },
                        PaymentMethodId = il.Invoice.PaymentMethodId,
                        PaymentMethod = new PaymentMethodDTO()
                        {
                            Id = il.Invoice.PaymentMethodId,
                            Name = il.Invoice.PaymentMethod.Name,
                            Since = il.Invoice.PaymentMethod.Since,
                            Until = il.Invoice.PaymentMethod.Until
                        },
                        TotalNet= il.Invoice.TotalNet,
                        TotalTax = il.Invoice.TotalTax,
                        TotalGross = il.Invoice.TotalGross
                    }
                    
                }).FirstOrDefaultAsync();
        }
        */
    }
}