using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IGpsSessionService GpsSessions { get; }
        IGpsLocationService GpsLocations { get; }
        IGpsLocationTypeService GpsLocationTypes { get; }
        IGpsSessionTypeService GpsSessionTypes { get; }
        ILangStrService LangStrs { get; }
        ILangStrTranslationService LangStrTranslation { get; }
        ITrackPointService TrackPoints { get; }
        ITrackService Tracks { get; }
    }
}