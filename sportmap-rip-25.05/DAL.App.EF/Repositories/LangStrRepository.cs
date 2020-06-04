using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Repositories
{
    public class LangStrRepository :
        EFBaseRepository<AppDbContext, DomainApp.Identity.AppUser, DomainApp.LangStr, DALAppDTO.LangStr>,
        ILangStrRepository
    {
        public LangStrRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<DomainApp.LangStr, DALAppDTO.LangStr>())
        {
        }
    }
}