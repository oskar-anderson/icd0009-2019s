using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MealRepository :  EFBaseRepository<Meal, AppDbContext>, IMealRepository
    {
        public MealRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}