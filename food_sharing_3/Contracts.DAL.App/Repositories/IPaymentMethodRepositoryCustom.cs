using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentMethodRepositoryCustom: IPaymentMethodRepositoryCustom<PaymentMethod>
    {
    }

    public interface IPaymentMethodRepositoryCustom<TPaymentMethod>
    {
    }

    
}