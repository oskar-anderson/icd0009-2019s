using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IGpsSessionService GpsSessions { get; }
        IGpsLocationService GpsLocations { get; }
        IGpsLocationTypeService LocationTypes { get; }
    }
}