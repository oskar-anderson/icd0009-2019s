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
        public IGpsLocationTypeService LocationTypes =>
            GetService<IGpsLocationTypeService>(() => new GpsLocationTypeService(UOW));
    }
}