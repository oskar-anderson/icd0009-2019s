using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserLocationRepository :  EFBaseRepository<UserLocation, AppDbContext>, IUserLocationRepository
    {
        public UserLocationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}