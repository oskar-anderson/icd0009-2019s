using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CartMealRepository : BaseRepository<CartMeal>, ICartMealRepository
    {
        public CartMealRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}