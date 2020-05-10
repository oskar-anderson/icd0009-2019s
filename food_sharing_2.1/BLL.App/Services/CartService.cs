using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Service;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class CartService : BaseEntityService<ICartRepository, IAppUnitOfWork, DAL.App.DTO.Cart, BLL.App.DTO.Cart>, ICartService
    {
        public CartService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Cart, BLL.App.DTO.Cart>(), unitOfWork.Carts)
        {
        }

        public async Task<IEnumerable<BLL.App.DTO.Cart>> AllAsync(Guid? userId = null)
        {
            return (await ServiceRepository.AllAsync(userId)).Select(dalEntity => Mapper.Map(dalEntity));
        }

        public async Task<BLL.App.DTO.Cart> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, userId));
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await ServiceRepository.ExistsAsync(id, userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            await ServiceRepository.DeleteAsync(id, userId);
        }
        /*
        public async Task<IEnumerable<CartDTO>> DTOAllAsync(Guid? userId = null)
        {
            return await ServiceRepository.DTOAllAsync(userId);
        }

        public async Task<CartDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            return await ServiceRepository.DTOFirstOrDefaultAsync(id, userId);
        }
        */
    }
}