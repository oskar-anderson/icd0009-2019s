using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserLocationRepository : BaseRepository<UserLocation>, IUserLocationRepository
    {
        public UserLocationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}