using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class SharingItemRepository :  EFBaseRepository<SharingItem, AppDbContext>, ISharingItemRepository
    {
        public SharingItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}