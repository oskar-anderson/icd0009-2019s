using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class LangStrTranslationService :
        BaseEntityService<IAppUnitOfWork, ILangStrTranslationRepository, ILangStrTranslationServiceMapper,
            DAL.App.DTO.LangStrTranslation, BLL.App.DTO.LangStrTranslation>, ILangStrTranslationService
    {
        public LangStrTranslationService(IAppUnitOfWork uow) : base(uow, uow.LangStrTranslations, new LangStrTranslationServiceMapper())
        {
        }
    }
}