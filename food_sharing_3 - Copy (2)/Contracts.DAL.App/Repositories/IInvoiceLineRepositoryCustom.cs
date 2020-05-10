using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IInvoiceLineRepositoryCustom: IInvoiceLineRepositoryCustom<InvoiceLine>
    {
    }

    public interface IInvoiceLineRepositoryCustom<TInvoiceLine>
    {
        Task<IEnumerable<TInvoiceLine>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}