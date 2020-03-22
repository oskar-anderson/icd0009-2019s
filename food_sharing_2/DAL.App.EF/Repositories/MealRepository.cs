using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ItemRepository : BaseRepository<InvoiceLine>, IItemRepository
    {
        public ItemRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}