using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ItemRepository : EFBaseRepository<Item, AppDbContext>, IItemRepository
    {
        public ItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}