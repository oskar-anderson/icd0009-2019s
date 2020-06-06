using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGpsSessionTypeRepository : IBaseRepository<GpsSessionType>, IGpsSessionTypeRepositoryCustom
    {
        
    }
}