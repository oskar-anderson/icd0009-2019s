using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BLL.App.Services
{
    public class GpsLocationService :
        BaseEntityService<IAppUnitOfWork, IGpsLocationRepository, IGpsLocationServiceMapper, DAL.App.DTO.GpsLocation,
            BLL.App.DTO.GpsLocation>, IGpsLocationService
    {
        public GpsLocationService(IAppUnitOfWork uow) : base(uow, uow.GpsLocations, new GpsLocationServiceMapper())
        {
        }

        public virtual async Task<IEnumerable<GpsLocation>> GetAllAsync(Guid gpsSessionId, Guid? userId = null,
            bool noTracking = true)
        {
            return (await Repository.GetAllAsync(gpsSessionId, userId, noTracking)).Select(e => Mapper.Map(e));
        }


        public virtual async Task<GpsLocation> AddAndUpdateSessionAsync(GpsLocation gpsLocation)
        {
            // get the last location from uow
            // calculate updated metrics for session
            // update session
            // add location

            var gpsSession = await UOW.GpsSessions.FirstOrDefaultAsync(gpsLocation.GpsSessionId, gpsLocation.AppUserId);
            var lastLocation = await UOW.GpsLocations.LastInSessionAsync(gpsSession.Id);
            if (lastLocation != null)
            {
                // calculate the metrics
                var distance = getDistance(gpsLocation, lastLocation);
                var vertical = getVerticalDistance(gpsLocation, lastLocation);
                gpsSession.Distance += distance;
                if (vertical < 0)
                {
                    gpsSession.Descent += Math.Abs(vertical);
                } else if (vertical > 0)
                {
                    gpsSession.Descent += vertical;
                }

                gpsSession.Duration = (gpsSession.RecordedAt - lastLocation.RecordedAt).TotalSeconds; 
                
                await UOW.GpsSessions.UpdateAsync(gpsSession);
            }
            
            return base.Add(gpsLocation);
        }

        private double getDistance(GpsLocation gpsLocation, DAL.App.DTO.GpsLocation lastLocation)
        {
            var d1 = gpsLocation.Latitude * (Math.PI / 180.0);
            var num1 = gpsLocation.Longitude * (Math.PI / 180.0);
            var d2 = lastLocation.Latitude * (Math.PI / 180.0);
            var num2 = lastLocation.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return Math.Abs(6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3))));
        }

        private double getVerticalDistance(GpsLocation gpsLocation, DAL.App.DTO.GpsLocation lastLocation)
        {
            return gpsLocation.Altitude - lastLocation.Altitude;
        }

    }
}