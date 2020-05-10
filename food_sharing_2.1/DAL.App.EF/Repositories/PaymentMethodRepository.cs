using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class PaymentMethodRepository :  EFBaseRepository<AppDbContext,  Domain.PaymentMethod, DAL.App.DTO.PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.PaymentMethod, DAL.App.DTO.PaymentMethod>())
        {
        }

        public async Task<IEnumerable<PaymentMethod>> AllAsync(Guid? userId = null)
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
            base.Remove(paymentMethod);
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