using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using ee.itcollege.kaande.pitsariina.BLL.Base.Services;


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

        public virtual async Task<IEnumerable<Cart>> GetAllForViewAsync(Guid userId)
        {
            return (await Repository.GetAllForViewAsync(userId)).Select(e => Mapper.MapCartView(e));
        }

        public virtual async Task<Cart> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapCartView(await Repository.FirstOrDefaultViewAsync(id, userId));
        }

        public virtual async Task<IEnumerable<Cart>> GetAllForApiAsync(Guid userId)
        {
            return (await Repository.GetAllForApiAsync(userId)).Select(e => Mapper.MapCartView(e));
        }

        public virtual async Task<Cart> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapCartView(await Repository.FirstOrDefaultApiAsync(id, userId));
        }
    }
}