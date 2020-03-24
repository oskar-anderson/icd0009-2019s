using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MealPriceRepository : BaseRepository<MealPrice>, IMealPriceRepository
    {
        public MealPriceRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}