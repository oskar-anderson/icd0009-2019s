using System;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        IGpsSessionRepository GpsSessions { get; }
        IGpsLocationRepository GpsLocations { get; }
        IGpsLocationTypeRepository LocationTypes { get; }
    }
}