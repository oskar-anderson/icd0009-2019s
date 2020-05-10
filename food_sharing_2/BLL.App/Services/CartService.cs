using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class CartService : 
        BaseEntityService<IAppUnitOfWork, ICartRepository, ICartServiceMapper, Domain.Base.App.DTO.Cart,
            BLL.App.DTO.Cart>, ICartService
    {
        public CartService(IAppUnitOfWork uow) : 
            base(uow, uow.Carts, new CartServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<BLL.App.DTO.Cart>> GetAllAsync(Guid Id, Guid? userId = null,
            bool noTracking = true)
        {
            return (await Repository.GetAllAsync(Id, userId, noTracking)).Select(e => Mapper.Map(e));
        }
    }
}