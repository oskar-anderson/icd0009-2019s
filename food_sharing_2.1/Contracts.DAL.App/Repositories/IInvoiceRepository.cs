using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        Task<IEnumerable<Invoice>> AllAsync(Guid? userId = null);
        Task<Invoice> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<InvoiceDTO>> DTOAllAsync(Guid? userId = null);
        // Task<InvoiceDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}