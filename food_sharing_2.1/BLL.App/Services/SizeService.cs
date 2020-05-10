using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Base.Mappers;
using BLL.Base.Service;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class SizeService : BaseEntityService<ISizeRepository, IAppUnitOfWork, DAL.App.DTO.Size, BLL.App.DTO.Size>, ISizeService
    {
        public SizeService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Size, BLL.App.DTO.Size>(), unitOfWork.Sizes)
        {
        }

        public async Task<IEnumerable<BLL.App.DTO.Size>> AllAsync(Guid? userId = null)
        {
            return (await ServiceRepository.AllAsync(userId)).Select(dalEntity => Mapper.Map(dalEntity));
        }

        public async Task<BLL.App.DTO.Size> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
        public async Task<IEnumerable<SizeDTO>> DTOAllAsync(Guid? userId = null)
        {
            return await ServiceRepository.DTOAllAsync(userId);
        }

        public async Task<SizeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            return await ServiceRepository.DTOFirstOrDefaultAsync(id, userId);
        }
        */
    }
}