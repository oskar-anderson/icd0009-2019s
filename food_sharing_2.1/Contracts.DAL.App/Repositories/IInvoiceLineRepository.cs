﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IInvoiceLineRepository : IBaseRepository<InvoiceLine>
    {
        Task<IEnumerable<InvoiceLine>> AllAsync(Guid? userId = null);
        Task<InvoiceLine> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<InvoiceLineDTO>> DTOAllAsync(Guid? userId = null);
        // Task<InvoiceLineDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}