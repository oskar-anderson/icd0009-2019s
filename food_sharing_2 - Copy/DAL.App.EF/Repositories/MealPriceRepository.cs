using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MealPriceRepository :  EFBaseRepository<MealPrice, AppDbContext>, IMealPriceRepository
    {
        public MealPriceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}