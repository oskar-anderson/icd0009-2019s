using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
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

        public IGpsLocationTypeRepository LocationTypes =>
            GetRepository<IGpsLocationTypeRepository>(() => new GpsLocationTypeRepository(UOWDbContext));

    }
}