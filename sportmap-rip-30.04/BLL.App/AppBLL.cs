using System;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.EF;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }

        public IGpsSessionService GpsSessions =>
            GetService<IGpsSessionService>(() => new GpsSessionService(UOW));

        public IGpsLocationService GpsLocations =>
            GetService<IGpsLocationService>(() => new GpsLocationService(UOW));

        public IGpsLocationTypeService GpsLocationTypes =>
            GetService<IGpsLocationTypeService>(() => new GpsLocationTypeService(UOW));

        public IGpsSessionTypeService GpsSessionTypes =>
            GetService<IGpsSessionTypeService>(() => new GpsSessionTypeService(UOW));

        public ILangStrService LangStrs =>
            GetService<ILangStrService>(() => new LangStrService(UOW));

        public ILangStrTranslationService LangStrTranslation =>
            GetService<ILangStrTranslationService>(() => new LangStrTranslationService(UOW));

        public ITrackPointService TrackPoints =>
            GetService<ITrackPointService>(() => new TrackPointService(UOW));

        public ITrackService Tracks =>
            GetService<ITrackService>(() => new TrackService(UOW));
    }
}