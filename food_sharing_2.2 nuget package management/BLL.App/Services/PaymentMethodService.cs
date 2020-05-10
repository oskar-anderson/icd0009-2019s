using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class PaymentMethodService :
        BaseEntityService<IAppUnitOfWork, IPaymentMethodRepository, IPaymentMethodServiceMapper, DAL.App.DTO.PaymentMethod,
            BLL.App.DTO.PaymentMethod>, IPaymentMethodService
    {
        public PaymentMethodService(IAppUnitOfWork uow) : 
            base(uow, uow.PaymentMethods, new PaymentMethodServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<BLL.App.DTO.PaymentMethod>> GetAllAsync(Guid Id, Guid? userId = null,
            bool noTracking = true)
        {
            return (await Repository.GetAllAsync(Id, userId, noTracking)).Select(e => Mapper.Map(e));
        }

    }
}