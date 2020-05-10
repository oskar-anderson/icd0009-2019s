using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Base.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentMethodRepositoryCustom: IPaymentMethodRepositoryCustom<PaymentMethod>
    {
    }

    public interface IPaymentMethodRepositoryCustom<TPaymentMethod>
    {
        Task<IEnumerable<TPaymentMethod>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}