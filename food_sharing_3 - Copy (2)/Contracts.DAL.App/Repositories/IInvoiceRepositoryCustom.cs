using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IInvoiceRepositoryCustom: IInvoiceRepositoryCustom<Invoice>
    {
    }

    public interface IInvoiceRepositoryCustom<TInvoice>
    {
        Task<IEnumerable<TInvoice>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}