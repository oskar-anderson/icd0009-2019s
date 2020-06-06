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
using DAL.App.EF;

namespace BLL.App.Services
{
    public class GpsSessionService :
        BaseEntityService<IAppUnitOfWork, IGpsSessionRepository, IGpsSessionServiceMapper, DAL.App.DTO.GpsSession,
            BLL.App.DTO.GpsSession>, IGpsSessionService
    {
        public GpsSessionService(IAppUnitOfWork uow) : base(uow, uow.GpsSessions, new GpsSessionServiceMapper())
        {
        }


        public virtual async Task<IEnumerable<GpsSessionView>> GetAllForViewAsync(int minLocationsCount = 10, double minDuration = 60, double minDistance = 10, DateTime? fromDateTime = null, DateTime? toDateTime = null)
        {
            return (await Repository.GetAllForViewAsync(minLocationsCount, minDuration, minDistance, fromDateTime, toDateTime)).Select(e => Mapper.MapGpsSessionView(e));
        }

        public async Task UpdateStatisticsAsync(Guid id)
        {
            // take the session, with all the locations. recalculate stats.
            var session = await  UOW.GpsSessions.GetFirstWithAllLocationsAsync(id);
            if (session == null) return;

            // reset stats
            session.Distance = 0;
            session.Duration = 0;
            session.Descent = 0;
            session.Climb = 0;
            session.Speed = 0;
            
            // calculate duration - in seconds
            if (session.GpsLocations!.Count > 1)
            {
                session.Duration = (session.GpsLocations.First().RecordedAt - session.GpsLocations.Last().RecordedAt).TotalSeconds;
            }

            DAL.App.DTO.GpsLocation? prevLocation = null;
            foreach (var gpsLocation in session.GpsLocations!)
            {
                if (prevLocation != null)
                {
                    var heightDif = gpsLocation.Altitude - prevLocation.Altitude;
                    if (heightDif > 0)
                    {
                        session.Climb += heightDif;
                    }
                    else
                    {
                        session.Descent += Math.Abs(heightDif);
                    }
                    session.Distance += GetDistance(gpsLocation, prevLocation);
                }

                

                prevLocation = gpsLocation;
            }

            // remove list
            session.GpsLocations = null;

            await UOW.GpsSessions.UpdateAsync(session);
            await UOW.SaveChangesAsync();
        }
        
        private static double GetDistance(DAL.App.DTO.GpsLocation gpsLocation, DAL.App.DTO.GpsLocation lastLocation)
        {
            var d1 = gpsLocation.Latitude * (Math.PI / 180.0);
            var num1 = gpsLocation.Longitude * (Math.PI / 180.0);
            var d2 = lastLocation.Latitude * (Math.PI / 180.0);
            var num2 = lastLocation.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return Math.Abs(6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3))));
        }
    }
}