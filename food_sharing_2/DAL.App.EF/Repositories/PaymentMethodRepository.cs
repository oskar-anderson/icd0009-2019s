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
    public class PaymentMethodRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.PaymentMethod, DTO.PaymentMethod>, 
        IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.PaymentMethod, DTO.PaymentMethod>())
        {
        }

        public async Task<IEnumerable<DTO.PaymentMethod>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .AsQueryable();
            
            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.PaymentMethod> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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