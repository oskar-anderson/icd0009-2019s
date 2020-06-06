using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGpsSessionRepositoryCustom: IGpsSessionRepositoryCustom<GpsSessionView>
    {
    }

    public interface IGpsSessionRepositoryCustom<TGpsSessionView>
    {
        Task<IEnumerable<TGpsSessionView>> GetAllForViewAsync();
    }
}