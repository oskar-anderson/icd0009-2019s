using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentMethodRepository : IBaseRepository<PaymentMethod>
    {
        Task<IEnumerable<PaymentMethod>> AllAsync(Guid? userId = null);
        Task<PaymentMethod> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<PaymentMethodDTO>> DTOAllAsync(Guid? userId = null);
        // Task<PaymentMethodDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}