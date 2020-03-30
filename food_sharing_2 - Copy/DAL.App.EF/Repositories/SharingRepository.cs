using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class SharingRepository :  EFBaseRepository<Sharing, AppDbContext>, ISharingRepository
    {
        public SharingRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}