using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.Base.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserLocationRepository  : IBaseRepository<UserLocation>, IUserLocationRepositoryCustom
    {
    }
}