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
    public class PaymentMethodRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.PaymentMethod, PaymentMethod>, 
        IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.PaymentMethod, PaymentMethod>())
        {
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .AsQueryable();
            
            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<PaymentMethod> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(cm => cm.Id == id)
                .AsQueryable();
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(cm => cm.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var paymentMethod = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(paymentMethod.Id);
        }

        /*
        public async Task<IEnumerable<PaymentMethodDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(pm => new PaymentMethodDTO()
                {
                    Id = pm.Id,
                    Name = pm.Name,
                    Since = pm.Since,
                    Until = pm.Until,
                    
                })
                .ToListAsync();
        }

        public async Task<PaymentMethodDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            PaymentMethodDTO cartMealDTO = await query
                .Select(pm => new PaymentMethodDTO()
                {
                    Id = pm.Id,
                    Name = pm.Name,
                    Since = pm.Since,
                    Until = pm.Until,
                })
                .FirstOrDefaultAsync();
            
            return cartMealDTO;
        }
        */

    }
}