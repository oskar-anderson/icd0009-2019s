using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IGpsSessionService : IBaseEntityService<GpsSession>, IGpsSessionRepositoryCustom<GpsSessionView>
    {
        Task UpdateStatisticsAsync(Guid id);
    }
}