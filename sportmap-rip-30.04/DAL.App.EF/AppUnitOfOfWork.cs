using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }

        public IGpsSessionRepository GpsSessions =>
            GetRepository<IGpsSessionRepository>(() => new GpsSessionRepository(UOWDbContext));

        public IGpsLocationRepository GpsLocations =>
            GetRepository<IGpsLocationRepository>(() => new GpsLocationRepository(UOWDbContext));

        public IGpsLocationTypeRepository GpsLocationTypes =>
            GetRepository<IGpsLocationTypeRepository>(() => new GpsLocationTypeRepository(UOWDbContext));

        public IGpsSessionTypeRepository GpsSessionTypes =>
            GetRepository<IGpsSessionTypeRepository>(() => new GpsSessionTypeRepository(UOWDbContext));

        public ILangStrRepository LangStrs =>
            GetRepository<ILangStrRepository>(() => new LangStrRepository(UOWDbContext));

        public ILangStrTranslationRepository LangStrTranslations =>
            GetRepository<ILangStrTranslationRepository>(() => new LangStrTranslationRepository(UOWDbContext));

        public ITrackPointRepository TrackPoints =>
            GetRepository<ITrackPointRepository>(() => new TrackPointRepository(UOWDbContext));

        public ITrackRepository Tracks =>
            GetRepository<ITrackRepository>(() => new TrackRepository(UOWDbContext));
    }
}