using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.kaande.pitsariina.Contracts.BLL.Base.Services;


namespace Contracts.BLL.App.Services
{
    public interface ISharingService : IBaseEntityService<Sharing>, ISharingRepositoryCustom<Sharing>
         {
        
    }
}