using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MenuMealRepository : BaseRepository<MenuMeal>, IMenuMealRepository
    {
        public MenuMealRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}