using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class SharingRepository : BaseRepository<Sharing>, ISharingRepository
    {
        public SharingRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}