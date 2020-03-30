using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MenuMealRepository :  EFBaseRepository<MenuMeal, AppDbContext>, IMenuMealRepository
    {
        public MenuMealRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}