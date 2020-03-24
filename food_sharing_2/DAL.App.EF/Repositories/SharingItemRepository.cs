using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class SharingItemRepository : BaseRepository<SharingItem>, ISharingItemRepository
    {
        public SharingItemRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}