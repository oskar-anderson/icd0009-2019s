using System;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        IGpsSessionRepository GpsSessions { get; }
        IGpsLocationRepository GpsLocations { get; }
        IGpsLocationTypeRepository GpsLocationTypes { get; }
        IGpsSessionTypeRepository GpsSessionTypes { get; }
        ILangStrRepository LangStrs { get; }
        ILangStrTranslationRepository LangStrTranslations { get; }
        ITrackPointRepository TrackPoints { get; }
        ITrackRepository Tracks { get; }
    }
}