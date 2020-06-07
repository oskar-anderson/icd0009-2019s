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
    public class UserLocationService : 
        BaseEntityService<IAppUnitOfWork, IUserLocationRepository, IUserLocationServiceMapper, DAL.App.DTO.UserLocation,
            BLL.App.DTO.UserLocation>, IUserLocationService
    {
        public UserLocationService(IAppUnitOfWork uow) : 
            base(uow, uow.UserLocations, new UserLocationServiceMapper())
        {
        }


        public virtual async Task<IEnumerable<UserLocation>> GetAllForViewAsync(Guid userId)
        {
            return (await Repository.GetAllForViewAsync(userId)).Select(e => Mapper.MapUserLocationView(e));
        }

        public virtual async Task<UserLocation> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapUserLocationView(await Repository.FirstOrDefaultViewAsync(id, userId));
        }

        public virtual async Task<IEnumerable<UserLocation>> GetAllForApiAsync(Guid userId)
        {
            return (await Repository.GetAllForViewAsync(userId)).Select(e => Mapper.MapUserLocationView(e));
        }

        public virtual async Task<UserLocation> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapUserLocationView(await Repository.FirstOrDefaultApiAsync(id, userId));
        }
    }
}