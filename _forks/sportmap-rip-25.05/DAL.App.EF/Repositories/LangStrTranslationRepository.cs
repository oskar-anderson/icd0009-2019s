using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class LangStrTranslationRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.LangStrTranslation,
            DAL.App.DTO.LangStrTranslation>,
        ILangStrTranslationRepository
    {
        public LangStrTranslationRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.LangStrTranslation, DTO.LangStrTranslation>())
        {
        }
    }
}