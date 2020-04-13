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
    public class PaymentMethodRepository :  EFBaseRepository<PaymentMethod, AppDbContext>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<PaymentMethod>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .AsQueryable();
            
            return await query.ToListAsync();
        }

        public async Task<PaymentMethod> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(cm => cm.Id == id)
                .AsQueryable();
            
            return await query.FirstOrDefaultAsync();
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
    }
}