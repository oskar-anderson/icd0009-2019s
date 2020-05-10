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
    public class UserLocationService : 
        BaseEntityService<IAppUnitOfWork, IUserLocationRepository, IUserLocationServiceMapper, DAL.App.DTO.UserLocation,
            BLL.App.DTO.UserLocation>, IUserLocationService
    {
        public UserLocationService(IAppUnitOfWork uow) : 
            base(uow, uow.UserLocations, new UserLocationServiceMapper())
        {
        }


        public virtual async Task<IEnumerable<UserLocation>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapUserLocationView(e));
        }
    }
}