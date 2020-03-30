using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CartRepository : EFBaseRepository<Cart, AppDbContext>, ICartRepository
    {
        public CartRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        // methods go here
        public override async Task<IEnumerable<Cart>> AllAsync()
        {
            return await RepoDbSet.Where(x => x.Meals.Count > 0).ToListAsync();
        }
    }
}