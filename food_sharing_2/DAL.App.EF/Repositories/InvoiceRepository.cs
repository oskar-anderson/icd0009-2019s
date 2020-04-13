using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class InvoiceRepository :  EFBaseRepository<Invoice, AppDbContext>, IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Invoice>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(i => i.PaymentMethod)
                .Include(i => i.Person)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(i => i.Person.AppUser.Id == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<Invoice> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(i => i.PaymentMethod)
                .Include(i => i.Person)
                .Where(a => a.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(i => i.Person.AppUser.Id == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(i => i.Id == id);
            }

            return await RepoDbSet.AnyAsync(i => i.Person.AppUser.Id == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var invoiceLine = await FirstOrDefaultAsync(id, userId);
            base.Remove(invoiceLine);
        }

        public async Task<IEnumerable<InvoiceDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(i => i.Person.AppUser.Id == userId);
            }

            return await query
                .Select(i => new InvoiceDTO()
                {
                    Id = i.Id,
                    PersonId = i.PersonId,
                    Person = new PersonDTO()
                    {
                        Id = i.Person.Id,
                        AppUserId = i.Person.AppUserId,
                        ThisIsMe = i.Person.ThisIsMe,
                        FirstName = i.Person.FirstName,
                        LastName = i.Person.LastName,
                        Phone = i.Person.Phone,
                        NationalIdentificationNumber = i.Person.NationalIdentificationNumber,
                        Since = i.Person.Since,
                        Until = i.Person.Until,
                    },
                    RestaurantId = i.RestaurantId,
                    Restaurant = new RestaurantDTO()
                    {
                        Id = i.Restaurant.Id,
                        Name = i.Restaurant.Name,
                        Location = i.Restaurant.Location,
                        Telephone = i.Restaurant.Telephone,
                        OpenTime = i.Restaurant.OpenTime,
                        OpenNotification = i.Restaurant.OpenNotification
                    },
                    PaymentMethodId = i.PaymentMethodId,
                    PaymentMethod = new PaymentMethodDTO()
                    {
                        Id = i.PaymentMethodId,
                        Name = i.PaymentMethod.Name,
                        Since = i.PaymentMethod.Since,
                        Until = i.PaymentMethod.Until
                    },
                    TotalNet= i.TotalNet,
                    TotalTax = i.TotalTax,
                    TotalGross = i.TotalGross
                })
                .ToListAsync();
        }

        public async Task<InvoiceDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(il => il.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(i => i.Person.AppUser.Id == userId);
            }
            
            return await query
                .Select(i => new InvoiceDTO()
                {
                    Id = i.Id,
                    PersonId = i.PersonId,
                    Person = new PersonDTO()
                    {
                        Id = i.Person.Id,
                        AppUserId = i.Person.AppUserId,
                        ThisIsMe = i.Person.ThisIsMe,
                        FirstName = i.Person.FirstName,
                        LastName = i.Person.LastName,
                        Phone = i.Person.Phone,
                        NationalIdentificationNumber = i.Person.NationalIdentificationNumber,
                        Since = i.Person.Since,
                        Until = i.Person.Until,
                    },
                    RestaurantId = i.RestaurantId,
                    Restaurant = new RestaurantDTO()
                    {
                        Id = i.Restaurant.Id,
                        Name = i.Restaurant.Name,
                        Location = i.Restaurant.Location,
                        Telephone = i.Restaurant.Telephone,
                        OpenTime = i.Restaurant.OpenTime,
                        OpenNotification = i.Restaurant.OpenNotification
                    },
                    PaymentMethodId = i.PaymentMethodId,
                    PaymentMethod = new PaymentMethodDTO()
                    {
                        Id = i.PaymentMethodId,
                        Name = i.PaymentMethod.Name,
                        Since = i.PaymentMethod.Since,
                        Until = i.PaymentMethod.Until
                    },
                    TotalNet= i.TotalNet,
                    TotalTax = i.TotalTax,
                    TotalGross = i.TotalGross
                }).FirstOrDefaultAsync();
        }
    }
}