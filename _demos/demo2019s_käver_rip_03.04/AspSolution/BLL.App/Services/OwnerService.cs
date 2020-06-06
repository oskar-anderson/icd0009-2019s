using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Mappers;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF;
using Domain;
using PublicApi.DTO.v1;

namespace BLL.App.Services
{
    public class OwnerService : BaseEntityService<IOwnerRepository, IAppUnitOfWork, Owner, Owner>, IOwnerService
    {
        public OwnerService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new IdentityMapper<Owner, Owner>(), unitOfWork.Owners)
        {
        }


        public async Task<IEnumerable<Owner>> AllAsync(Guid? userId = null) =>
            await ServiceRepository.AllAsync(userId);

        public async Task<Owner> FirstOrDefaultAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.FirstOrDefaultAsync(id, userId);

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.ExistsAsync(id, userId);

        public async Task DeleteAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.DeleteAsync(id, userId);

        public async Task<IEnumerable<OwnerDTO>> DTOAllAsync(Guid? userId = null) =>
            await ServiceRepository.DTOAllAsync(userId);

        public async Task<OwnerDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.DTOFirstOrDefaultAsync(id, userId);
    }
}