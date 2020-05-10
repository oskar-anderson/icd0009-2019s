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
        BaseEntityService<IAppUnitOfWork, ICartRepository, ICartServiceMapper, DAL.App.DTO.Cart,
            BLL.App.DTO.Cart>, ICartService
    {
        public CartService(IAppUnitOfWork uow) : 
            base(uow, uow.Carts, new CartServiceMapper())
        {
        }

        public virtual async Task<IEnumerable<Cart>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapCartView(e));
        }
    }
}