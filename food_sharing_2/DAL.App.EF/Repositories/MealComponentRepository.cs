using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MealComponentRepository : BaseRepository<MealComponent>, IMealComponentRepository
    {
        public MealComponentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}