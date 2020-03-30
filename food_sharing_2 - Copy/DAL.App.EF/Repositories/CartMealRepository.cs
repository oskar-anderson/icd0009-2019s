using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CartMealRepository : EFBaseRepository<CartMeal, AppDbContext>, ICartMealRepository
    {
        public CartMealRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}