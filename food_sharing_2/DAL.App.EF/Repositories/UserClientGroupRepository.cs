using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class SizeRepository : BaseRepository<Size>, ISizeRepository
    {
        public SizeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}